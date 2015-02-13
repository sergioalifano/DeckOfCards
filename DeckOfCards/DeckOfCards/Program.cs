using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeckOfCards
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck myDeck=new Deck(3);
            myDeck.Deal(4);

            myDeck.Shuffle();
            Console.ReadKey();
        }
    }


    // When a new deck is created, you’ll create a card of each rank for each suit and add them to the deck of cards, 
    //      which in this case will be a List of Card objects.
    //
    // A deck can perform the following actions:
	//     void Shuffle() -- Merges the discarded pile with the deck and shuffles the cards
	//     List<card> Deal(int numberOfCards) - returns a number of cards from the top of the deck
	//     void Discard(Card card) / void Discard(List<Card> cards) - returns a card from a player to the 
	//         discard pile	
    // 
    // A deck knows the following information about itself:
	//     int CardsRemaining -- number of cards left in the deck
	//     List<Card> DeckOfCards -- card waiting to be dealt
    //     List<Card> DiscardedCards -- cards that have been played
    class Deck
    {
        public List<Card> DeckOfCards;
        public List<Card> DiscardedCards;
        public int CardsRemaining { get; set; }

        public Deck()
        {
            DeckOfCards = new List<Card>();
            DiscardedCards = new List<Card>();

            AddCardsToDeck();
            CardsRemaining = DeckOfCards.Count();
        }

        /// <summary>
        /// Overload constructor
        /// </summary>
        /// <param name="numberOfDeck"></param>
        public Deck(int numberOfDeck)
            :this() //call the base constructor
        {
            for (int i = 0; i < numberOfDeck-1; i++)
            {
                AddCardsToDeck();
            }
            CardsRemaining = DeckOfCards.Count();
        }

        /// <summary>
        ///  Merges the discarded pile with the deck and shuffles the cards
        /// </summary>
        public void Shuffle()
        {
            List<Card> shuffled = new List<Card>();
             int randomCard;

            //merge together the the decks of cards  P.S.The function Concat() returns a new list.
             DeckOfCards = (List<Card>)DeckOfCards.Concat(DiscardedCards).ToList();

            Random gnr=new Random();
            for (int i = 0; i < DeckOfCards.Count; i++)
            {
                randomCard = gnr.Next(0, DeckOfCards.Count);

                //add the random card to the shuffled deck
                shuffled.Add(DeckOfCards[randomCard]);

                //remove from the original deck the card
                DeckOfCards.RemoveAt(randomCard);
            }

            DeckOfCards = shuffled;
        }

        /// <summary>
        /// Returns a number of cards from the top of the deck
        /// </summary>
        /// <param name="numberOfCards">number of card</param>
        /// <returns>List of cards</returns>
        public List<Card> Deal(int numberOfCards)
        {
            List<Card> myCards = new List<Card>();

            //if there are enough cards from the deck
            if (CardsRemaining > numberOfCards)
            {
                //save cards into another list
                myCards = DeckOfCards.Take(numberOfCards).ToList();

                //remove cards from the deck
                DeckOfCards.RemoveRange(0, numberOfCards);

                CardsRemaining = DeckOfCards.Count();
            }
            else
            {
                //just take the remaining cards
                myCards = DeckOfCards.Take(CardsRemaining).ToList();

                //remove cards from the deck
                DeckOfCards.RemoveRange(0, CardsRemaining);

                CardsRemaining = DeckOfCards.Count();
            }

            return myCards;
        }

        /// <summary>
        /// Returns a card from a player to the discard pile
        /// </summary>
        /// <param name="card"></param>
        public void Discard(Card card)
        {
            DiscardedCards.Add(card);
        }

        public void Discard(List<Card> cards)
        {
            DiscardedCards = (List<Card>)DiscardedCards.Concat(cards).ToList();
        }

        /// <summary>
        /// Adds all the Suits and Ranks to the deck
        /// </summary>
        public void AddCardsToDeck()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    DeckOfCards.Add(new Card(suit, rank));
                }
            }
        }
    }

    
    // What makes a card?
	//     A card is comprised of it’s suit and its rank.  Both of which are enumerations.
    //     These enumerations should be "Suit" and "Rank"
    class Card
    {
        public Suit Suit { get; set; }
        public Rank Rank { get; set; }

        public Card(Suit suit,Rank rank)
        {
            this.Suit = suit;
            this.Rank = rank;
        }
    }

    public enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    public enum Rank
    {
        Two=2, Three, Four, Five, Six, Seven, Eigth, Nine, Ten, Jack, Queen, King, Ace
    }
}

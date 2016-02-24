using System;
using System.Collections.Generic;
using System.Linq;

namespace EulersSolver.Problems
{
    internal class Problem54 : BaseProblem
    {
        protected override int ProblemNumber => 54;
        protected override bool HasBeenSolved => true;

        public class PokerCard
        {
            public Suit Suit;
            public int CardValue;
        }

        public class Hand
        {
            public List<PokerCard> Cards;
            public PokerHandType HandType;
        }

        public enum Suit
        {
            Clubs,
            Diamonds,
            Hearts,
            Spades
        }

        public enum PokerHandType
        {
            HighCard = 1,
            Onepair = 2,
            TwoPair = 3,
            ThreeOfAKind = 4,
            Straight = 5,
            Flush = 6,
            FullHouse = 7,
            FourOfAKind = 8,
            StraightFlush = 9,
            RoyalFlush = 10
        }

        protected override void Solve()
        {
            /*
               Poker hands
            Problem 54
            In the card game poker, a hand consists of five cards and are ranked, from lowest to highest, in the following way:

            High Card: Highest value card.
            One Pair: Two cards of the same value.
            Two Pairs: Two different pairs.
            Three of a Kind: Three cards of the same value.
            Straight: All cards are consecutive values.
            Flush: All cards of the same Suit.
            Full House: Three of a kind and a pair.
            Four of a Kind: Four cards of the same value.
            Straight Flush: All cards are consecutive values of same Suit.
            Royal Flush: Ten, Jack, Queen, King, Ace, in same Suit.
            The cards are valued in the order:
            2, 3, 4, 5, 6, 7, 8, 9, 10, Jack, Queen, King, Ace.

            If two players have the same ranked hands then the rank made up of the highest value wins; for example, a pair of eights beats a pair of fives (see example 1 below). But if two ranks tie, for example, both players have a pair of queens, then highest cards in each hand are compared (see example 4 below); if the highest cards tie then the next highest cards are compared, and so on.

            Consider the following five hands dealt to two players:

            Hand	 	Player 1	 	Player 2	 	Winner
            1	 	5H 5C 6S 7S KD
            Pair of Fives
 	            2C 3S 8S 8D TD
            Pair of Eights
 	            Player 2
            2	 	5D 8C 9S JS AC
            Highest card Ace
 	            2C 5C 7D 8S QH
            Highest card Queen
 	            Player 1
            3	 	2D 9C AS AH AC
            Three Aces
 	            3D 6D 7D TD QD
            Flush with Diamonds
 	            Player 2
            4	 	4D 6S 9H QH QC
            Pair of Queens
            Highest card Nine
 	            3D 6D 7H QD QS
            Pair of Queens
            Highest card Seven
 	            Player 1
            5	 	2H 2D 4C 4D 4S
            Full House
            With Three Fours
 	            3C 3D 3S 9S 9D
            Full House
            with Three Threes
 	            Player 1
            The file, poker.txt, contains one-thousand random hands dealt to two players. Each line of the file contains ten cards (separated by a single space): the first five are Player 1's cards and the last five are Player 2's cards. You can assume that all hands are valid (no invalid characters or repeated cards), each player's hand is in no specific order, and in each hand there is a clear winner.

            How many hands does Player 1 win?
            */

            Initialize();

            const string problemTextfilePath = @"..\..\..\Resources\p054_poker.txt";
            var file = new System.IO.StreamReader(problemTextfilePath);

            string line;
            var count = 0;
            while ((line = file.ReadLine()) != null)
            {
                var dealtCards = line.Split(' ');
                if (FirstPlayerWon(dealtCards))
                {
                    count++;
                }
            }

            Finalize(count);
        }

        private static bool DeterminePlayerOneHighCardWinner(Hand player1, Hand player2)
        {
            if (player1.Cards[4].CardValue > player2.Cards[4].CardValue)
            {
                return true;
            }
            else if (player1.Cards[4].CardValue == player2.Cards[4].CardValue)
            {
                if (player1.Cards[3].CardValue > player2.Cards[3].CardValue)
                {
                    return true;
                }
                else if (player1.Cards[3].CardValue == player2.Cards[3].CardValue)
                {
                    if (player1.Cards[2].CardValue > player2.Cards[2].CardValue)
                    {
                        return true;
                    }
                    else if (player1.Cards[2].CardValue == player2.Cards[2].CardValue)
                    {
                        if (player1.Cards[1].CardValue > player2.Cards[1].CardValue)
                        {
                            return true;
                        }
                        else if (player1.Cards[1].CardValue == player2.Cards[1].CardValue)
                        {
                            return player1.Cards[0].CardValue > player2.Cards[0].CardValue;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private static bool DeterminePlayerOneOnePairWinner(Hand player1, Hand player2)
        {
            int player1Highcard, player2Highcard;

            if (player1.Cards[0].CardValue == player1.Cards[1].CardValue)
            {
                player1Highcard = player1.Cards[0].CardValue;
            }
            else if (player1.Cards[1].CardValue == player1.Cards[2].CardValue)
            {
                player1Highcard = player1.Cards[1].CardValue;
            }
            else if (player1.Cards[2].CardValue == player1.Cards[3].CardValue)
            {
                player1Highcard = player1.Cards[2].CardValue;
            }
            else if (player1.Cards[3].CardValue == player1.Cards[4].CardValue)
            {
                player1Highcard = player1.Cards[3].CardValue;
            }
            else
            {
                player1Highcard = player1.Cards[4].CardValue;
            }

            if (player2.Cards[0].CardValue == player2.Cards[1].CardValue)
            {
                player2Highcard = player2.Cards[0].CardValue;
            }
            else if (player2.Cards[1].CardValue == player2.Cards[2].CardValue)
            {
                player2Highcard = player2.Cards[1].CardValue;
            }
            else if (player2.Cards[2].CardValue == player2.Cards[3].CardValue)
            {
                player2Highcard = player2.Cards[2].CardValue;
            }
            else if (player2.Cards[3].CardValue == player2.Cards[4].CardValue)
            {
                player2Highcard = player2.Cards[3].CardValue;
            }
            else
            {
                player2Highcard = player2.Cards[4].CardValue;
            }

            return player1Highcard > player2Highcard;
        }

        private static bool DeterminePlayerOneTwoPairWinner(Hand player1, Hand player2)
        {
            int player1Highcard, player2Highcard;

            if (player1.Cards[0].CardValue == player1.Cards[1].CardValue && player1.Cards[2].CardValue == player1.Cards[3].CardValue)
            {
                player1Highcard = player1.Cards[0].CardValue > player1.Cards[2].CardValue ? player1.Cards[0].CardValue : player1.Cards[2].CardValue;
            }
            else if (player1.Cards[0].CardValue == player1.Cards[1].CardValue && player1.Cards[3].CardValue == player1.Cards[4].CardValue)
            {
                player1Highcard = player1.Cards[0].CardValue > player1.Cards[3].CardValue ? player1.Cards[0].CardValue : player1.Cards[3].CardValue;
            }
            else //if (player1.Cards[1].CardValue == player1.Cards[2].CardValue && player1.Cards[3].CardValue == player1.Cards[4].CardValue)
            {
                player1Highcard = player1.Cards[1].CardValue > player1.Cards[3].CardValue ? player1.Cards[1].CardValue : player1.Cards[3].CardValue;
            }

            if (player2.Cards[0].CardValue == player2.Cards[1].CardValue && player2.Cards[2].CardValue == player2.Cards[3].CardValue)
            {
                player2Highcard = player2.Cards[0].CardValue > player2.Cards[2].CardValue ? player2.Cards[0].CardValue : player2.Cards[2].CardValue;
            }
            else if (player2.Cards[0].CardValue == player2.Cards[1].CardValue && player2.Cards[3].CardValue == player2.Cards[4].CardValue)
            {
                player2Highcard = player2.Cards[0].CardValue > player2.Cards[3].CardValue ? player2.Cards[0].CardValue : player2.Cards[3].CardValue;
            }
            else //if (player2.Cards[1].CardValue == player2.Cards[2].CardValue && player2.Cards[3].CardValue == player2.Cards[4].CardValue)
            {
                player2Highcard = player2.Cards[1].CardValue > player2.Cards[3].CardValue ? player2.Cards[1].CardValue : player2.Cards[3].CardValue;
            }

            return player1Highcard > player2Highcard;
        }

        private static bool DeterminePlayerOneThreeOfAKindWinner(Hand player1, Hand player2)
        {
            int player1Highcard, player2Highcard;

            if (player1.Cards[0].CardValue == player1.Cards[1].CardValue && player1.Cards[1].CardValue == player1.Cards[2].CardValue && player1.Cards[2].CardValue == player1.Cards[3].CardValue)
            {
                player1Highcard = player1.Cards[0].CardValue;
            }
            else if (player1.Cards[1].CardValue == player1.Cards[2].CardValue && player1.Cards[2].CardValue == player1.Cards[3].CardValue && player1.Cards[3].CardValue == player1.Cards[4].CardValue)
            {
                player1Highcard = player1.Cards[1].CardValue;
            }
            else //if (player1.Cards[2].CardValue == player1.Cards[3].CardValue && player1.Cards[3].CardValue == player1.Cards[4].CardValue && player1.Cards[4].CardValue == player1.Cards[5.CardValue)
            {
                player1Highcard = player1.Cards[2].CardValue;
            }

            if (player2.Cards[0].CardValue == player2.Cards[1].CardValue && player2.Cards[1].CardValue == player2.Cards[2].CardValue && player2.Cards[2].CardValue == player2.Cards[3].CardValue)
            {
                player2Highcard = player2.Cards[0].CardValue > player2.Cards[2].CardValue ? player2.Cards[0].CardValue : player2.Cards[2].CardValue;
            }
            else if (player2.Cards[1].CardValue == player2.Cards[2].CardValue && player2.Cards[2].CardValue == player2.Cards[3].CardValue && player2.Cards[3].CardValue == player2.Cards[4].CardValue)
            {
                player2Highcard = player2.Cards[0].CardValue > player2.Cards[3].CardValue ? player2.Cards[0].CardValue : player2.Cards[3].CardValue;
            }
            else //if (player2.Cards[2].CardValue == player2.Cards[3].CardValue && player2.Cards[3].CardValue == player2.Cards[4].CardValue && player2.Cards[4].CardValue == player2.Cards[5.CardValue)
            {
                player2Highcard = player2.Cards[1].CardValue > player2.Cards[3].CardValue ? player2.Cards[1].CardValue : player2.Cards[3].CardValue;
            }

            return player1Highcard > player2Highcard;
        }

        private static bool DeterminePlayerOneStraightOrFlushWinner(Hand player1, Hand player2)
        {
            return player1.Cards[4].CardValue > player2.Cards[4].CardValue;
        }

        private static bool DeterminePlayerOneFullHouseWinner(Hand player1, Hand player2)
        {
            int player1Highcard, player2Highcard;

            if (player1.Cards[0].CardValue == player1.Cards[1].CardValue && player1.Cards[1].CardValue == player1.Cards[2].CardValue && player1.Cards[2].CardValue == player1.Cards[3].CardValue &&
                player1.Cards[4].CardValue == player1.Cards[5].CardValue)
            {
                player1Highcard = player1.Cards[0].CardValue > player1.Cards[4].CardValue ? player1.Cards[0].CardValue : player1.Cards[4].CardValue;
            }
            else// if (player1.Cards[0].CardValue == player1.Cards[1].CardValue &&
            //     player1.Cards[2].CardValue == player1.Cards[3].CardValue && player1.Cards[3].CardValue == player1.Cards[4].CardValue && player1.Cards[4].CardValue == player1.Cards[5].CardValue)
            {
                player1Highcard = player1.Cards[0].CardValue > player1.Cards[2].CardValue ? player1.Cards[0].CardValue : player1.Cards[2].CardValue;
            }

            if (player2.Cards[0].CardValue == player2.Cards[1].CardValue && player2.Cards[1].CardValue == player2.Cards[2].CardValue && player2.Cards[2].CardValue == player2.Cards[3].CardValue &&
                player2.Cards[4].CardValue == player2.Cards[5].CardValue)
            {
                player2Highcard = player2.Cards[0].CardValue > player2.Cards[4].CardValue ? player2.Cards[0].CardValue : player2.Cards[4].CardValue;
            }
            else// if (player2.Cards[0].CardValue == player2.Cards[1].CardValue &&
            //     player2.Cards[2].CardValue == player2.Cards[3].CardValue && player2.Cards[3].CardValue == player2.Cards[4].CardValue && player2.Cards[4].CardValue == player2.Cards[5].CardValue)
            {
                player2Highcard = player2.Cards[0].CardValue > player2.Cards[2].CardValue ? player2.Cards[0].CardValue : player2.Cards[2].CardValue;
            }

            return player1Highcard > player2Highcard;
        }

        private static bool DeterminePlayerOneFourOfAKindWinner(Hand player1, Hand player2)
        {
            int player1Highcard, player2Highcard;

            if (player1.Cards[0].CardValue == player1.Cards[1].CardValue && player1.Cards[1].CardValue == player1.Cards[2].CardValue && player1.Cards[2].CardValue == player1.Cards[3].CardValue &&
                player1.Cards[3].CardValue == player1.Cards[4].CardValue)
            {
                player1Highcard = player1.Cards[0].CardValue;
            }
            else// if (player1.Cards[1].CardValue == player1.Cards[2].CardValue && player1.Cards[2].CardValue == player1.Cards[3].CardValue && player1.Cards[3].CardValue == player1.Cards[4].CardValue &&
            //    player1.Cards[4].CardValue == player1.Cards[5].CardValue)
            {
                player1Highcard = player1.Cards[1].CardValue;
            }

            if (player2.Cards[0].CardValue == player2.Cards[1].CardValue && player2.Cards[1].CardValue == player2.Cards[2].CardValue && player2.Cards[2].CardValue == player2.Cards[3].CardValue &&
                    player2.Cards[3].CardValue == player2.Cards[4].CardValue)
            {
                player2Highcard = player2.Cards[0].CardValue;
            }
            else// if (player2.Cards[1].CardValue == player2.Cards[2].CardValue && player2.Cards[2].CardValue == player2.Cards[3].CardValue && player2.Cards[3].CardValue == player2.Cards[4].CardValue &&
            //    player2.Cards[4].CardValue == player2.Cards[5].CardValue)
            {
                player2Highcard = player2.Cards[1].CardValue;
            }

            return player1Highcard > player2Highcard;
        }

        private static bool DetermineIfPlayerOneHasHighCard(Hand player1, Hand player2)
        {
            const bool player1Victory = false;

            switch (player1.HandType)
            {
                case PokerHandType.HighCard:
                    return DeterminePlayerOneHighCardWinner(player1, player2);

                case PokerHandType.Onepair:
                    return DeterminePlayerOneOnePairWinner(player1, player2);

                case PokerHandType.TwoPair:
                    return DeterminePlayerOneTwoPairWinner(player1, player2);

                case PokerHandType.ThreeOfAKind:
                    return DeterminePlayerOneThreeOfAKindWinner(player1, player2);

                case PokerHandType.StraightFlush:
                case PokerHandType.RoyalFlush:
                case PokerHandType.Straight:
                case PokerHandType.Flush:
                    return DeterminePlayerOneStraightOrFlushWinner(player1, player2);

                case PokerHandType.FullHouse:
                    return DeterminePlayerOneFullHouseWinner(player1, player2);

                case PokerHandType.FourOfAKind:
                    return DeterminePlayerOneFourOfAKindWinner(player1, player2);
            }
            return player1Victory;
        }

        private static bool FirstPlayerWon(string[] dealtCards)
        {
            var player1Cards = dealtCards.Take(dealtCards.Length / 2).ToArray();
            var player2Cards = dealtCards.Skip(dealtCards.Length / 2).ToArray();

            var player1 = DetermineHand(player1Cards);
            var player2 = DetermineHand(player2Cards);

            var player1HandValue = Convert.ToInt32(player1.HandType);
            var player2Handvalue = Convert.ToInt32(player2.HandType);

            if (player1HandValue == player2Handvalue)
            {
                return DetermineIfPlayerOneHasHighCard(player1, player2);
            }

            return player1HandValue > player2Handvalue;
        }

        private static Hand DetermineHand(IEnumerable<string> cardsInHands)
        {
            var returnHand = new Hand { Cards = new List<PokerCard>() };

            foreach (var value in cardsInHands)
            {
                var card = new PokerCard
                {
                    CardValue = DetermineCardValue(value[0]),
                    Suit = DetermineSuit(value[1])
                };
                returnHand.Cards.Add(card);
            }

            returnHand.Cards = returnHand.Cards.OrderBy(t => t.CardValue).ToList();
            returnHand.HandType = DetermineHandType(returnHand);
            return returnHand;
        }

        private static int DetermineCardValue(char value)
        {
            switch (value)
            {
                case '1': return 1;
                case '2': return 2;
                case '3': return 3;
                case '4': return 4;
                case '5': return 5;
                case '6': return 6;
                case '7': return 7;
                case '8': return 8;
                case '9': return 9;
                case 'T': return 10;
                case 'J': return 11;
                case 'Q': return 12;
                case 'K': return 13;
                case 'A': return 14;
                default: return 0;
            }
        }

        private static Suit DetermineSuit(char value)
        {
            switch (value)
            {
                case 'C': return Suit.Clubs;
                case 'D': return Suit.Diamonds;
                case 'H': return Suit.Hearts;
                default:
                case 'S': return Suit.Spades;
            }
        }

        private static PokerHandType DetermineHandType(Hand playerHand)
        {
            PokerHandType type;
            // These should be in ascending order since we ordered the cards when we created the hand; we'll use that fact to our advantage here
            var card1 = playerHand.Cards[0];
            var card2 = playerHand.Cards[1];
            var card3 = playerHand.Cards[2];
            var card4 = playerHand.Cards[3];
            var card5 = playerHand.Cards[4];

            var flushConditions = card1.Suit == card2.Suit &&
                                   card2.Suit == card3.Suit &&
                                   card3.Suit == card4.Suit &&
                                   card4.Suit == card5.Suit;

            var straightConditions = card1.CardValue + 1 == card2.CardValue &&
                                      card2.CardValue + 1 == card3.CardValue &&
                                      card3.CardValue + 1 == card4.CardValue &&
                                      card4.CardValue + 1 == card5.CardValue;

            if (flushConditions)
            {
                type = PokerHandType.Flush;

                if (!straightConditions) return type;
                type = PokerHandType.StraightFlush;

                if (card1.CardValue == 10)
                {
                    type = PokerHandType.RoyalFlush;
                }
            }
            else
            {
                // xxxx., .xxxx
                var fourOfAKind = (card1.CardValue == card2.CardValue &&
                                    card2.CardValue == card3.CardValue &&
                                    card3.CardValue == card4.CardValue)
                                    ||
                                    (card2.CardValue == card3.CardValue &&
                                    card3.CardValue == card4.CardValue &&
                                    card4.CardValue == card5.CardValue);

                if (straightConditions)
                {
                    type = PokerHandType.Straight;
                }
                else if (fourOfAKind)
                {
                    type = PokerHandType.FourOfAKind;
                }
                else
                {
                    // xxxyy, yyxxx
                    var fullHouse = (card1.CardValue == card2.CardValue &&
                                      card3.CardValue == card4.CardValue &&
                                      card4.CardValue == card5.CardValue)
                                      ||
                                      (card1.CardValue == card2.CardValue &&
                                      card2.CardValue == card3.CardValue &&
                                      card4.CardValue == card5.CardValue);
                    if (fullHouse)
                    {
                        type = PokerHandType.FullHouse;
                    }
                    else
                    {
                        // xxx.., .xxx., ..xxx
                        var threeOfAKind = (card1.CardValue == card2.CardValue &&
                                            card2.CardValue == card3.CardValue)
                                            ||
                                            (card2.CardValue == card3.CardValue &&
                                            card3.CardValue == card4.CardValue)
                                            ||
                                            (card3.CardValue == card4.CardValue &&
                                            card4.CardValue == card5.CardValue);

                        if (threeOfAKind)
                        {
                            type = PokerHandType.ThreeOfAKind;
                        }
                        else
                        {
                            // xxyy., xx.yy, .xxyy
                            var twoPair = (card1.CardValue == card2.CardValue &&
                                            card3.CardValue == card4.CardValue)
                                            ||
                                            (card1.CardValue == card2.CardValue &&
                                            card4.CardValue == card5.CardValue)
                                            ||
                                            (card2.CardValue == card3.CardValue &&
                                            card4.CardValue == card5.CardValue);
                            if (twoPair)
                            {
                                type = PokerHandType.TwoPair;
                            }
                            else
                            {
                                // xx..., .xx.., ..xx., ...xx
                                var onepair = card1.CardValue == card2.CardValue ||
                                                  card2.CardValue == card3.CardValue ||
                                                  card3.CardValue == card4.CardValue ||
                                                  card4.CardValue == card5.CardValue;
                                type = onepair ? PokerHandType.Onepair : PokerHandType.HighCard;
                            }
                        }
                    }
                }
            }

            return type;
        }
    }
}
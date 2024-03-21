# **Programming test**

## Texas Hold'em all-in app!


![](https://i.gifer.com/8qIJ.gif)




This project is a C# test to a backend position.

The ideia is a Texas Hold'em poker app, but due to the Texas Hold'em complexity, the app was limited to simulate an all-in round of a six player table.

The pot, stack amount, positions and all game flows were put aside: just one round, everybody all in, and the winner hand at the end is showed.

## Pre requisites
I'll need **.NET SDK.** You can download and install the .NET SDK directly from the [official .NET website](https://dotnet.microsoft.com/download)

## Execution
  ```shell
    dotnet restore
    dotnet build
    dotnet run  
   ```

## Test overview

The test expect the correct application of the Texas hold'em hand ranking alogside a good application of algorithms in C#, OOP, test covering, API calls and mainly the will of learn, since C# wasn't in my daily usage as a programming language.


## Application flow

When application starts, a prompt asks for a player name, and then for a game modality: heads-up(two players), 6-max(6 players) and full-ring(9 players). The other one, five or eight names the app gets calling [some fake name api](https://api.namefake.com/). The players and the dealer are instantiated and the players start to receive their cards, one card to each one, just like a texas hold'em card distribution. Right after this, the flop, turn and river are placed in the table. The Hand and Pontuation classes are called with seven cards to each player(2 from player and 5 from the table) and they should return the best game that can be made with just 5 of the 7 cards, following the [Texas Hold'em hand ranking](https://upswingpoker.com/poker-hands-rankings/).
In the hand with the highest score among the players is shown.

## Technologies
The project was built in `C#`(calm down, this was my first touch at this language) and `.NET` version 8.0, in addition to using `Moq` and `Xunit` libs to test.

## Score calculation (5 cards made hand)

 - #### Straight Flush (suited sequence): 
    - `900000 + highestSequenceCard.Score`

      All five cards in sequence, same suit

 - #### Quadra (Four of a Kind): 
    - `800000 + fourOfAKindCard.Score + kicker.Score`

      Four cards of the same rank, one not

 - #### Full House (Full House): 
    - `700000 + (threeOfAKindCard.Score * 10) + pairCard.Score`

      Three equal rank cards plus two equal card of other rank 

 - #### Flush (Cor): 
    -` 600000 + (highestCard.Score * 3000) + (secondCard.Score * 1000) + (thirdCard.Score * 100) + (fourthCard.Score * 10) + fifthCard.Score`

    Five cards of the same suit

 - #### Sequência (Straight): 
    - `500000 + highestSequeceCard.Score`

    Five cards with sequenced ranks

 - #### Trinca (Three of a Kind): 
    - `400000 + (threeOfAKindCard.Score * 3000) + (fourthCard.Score * 1000) + (fifthCard.Score * 100)`

    Three cards with the same rank, to differen`

 - #### Dois Pares (Two Pair): 
    - `300000 + (highestPairCard.Score * 100) + (lowestPairCard.Score * 10) + fifthCard.Score`

    Two cards of the same rank, another two cards with same rank(different from first two), and another different one

 - #### Um Par (One Pair): 
    - `200000 + (pairCard.Score * 3000) + (thirdCard.Score * 1000) + (forthCard.Score * 100) + (fifthCard * 10)`

    Two cards with the same rank, three different

 - #### Carta Alta (High Card): 
    - `(highestCard.Score * 10000) + (secondCard.Score * 1000) + (thirdCard.Score * 100) + (forthCard.Score * 10) + fifthCard.Score`

   Any hand that does not fit into any hand above

#pragma warning disable CS0436 // Type conflicts with imported type
namespace TexasHoldemTests;

using TexasHoldem;

public class GameTests
{
  public readonly string BADASS_DRUMMER_NAME_FOR_FAKE_PLAYER = "Mario Duplantier";
  public readonly string BADASS_GUITARIST_NAME_FOR_FAKE_PLAYER = "John Petrucci";

  [Fact]
  public void ShouldFinishTheGame_OneWinner()
  {
    //set players
    List<Player> players = [
      new RealPlayer(BADASS_DRUMMER_NAME_FOR_FAKE_PLAYER),
      new FakePlayer(BADASS_GUITARIST_NAME_FOR_FAKE_PLAYER)
    ];
    List<Card> marioCards = HandHelper.GetCardsByNotation(HandHelper.SeparateCardsNotations("AsAc"));
    List<Card> johnCards = HandHelper.GetCardsByNotation(HandHelper.SeparateCardsNotations("2s3c"));
    players[0].ReceiveCard(marioCards[0]);
    players[0].ReceiveCard(marioCards[1]);
    players[1].ReceiveCard(johnCards[0]);
    players[1].ReceiveCard(johnCards[1]);

    //set table
    Table table = new();
    List<Card> flopCards = HandHelper.GetCardsByNotation(HandHelper.SeparateCardsNotations("AhAsTs"));
    Card turnCard = new("9", "Hearts");
    Card riverCard = new("5", "Clubs");
    table.ReceiveFlop(flopCards);
    table.ReceiveTurn(turnCard);
    table.ReceiveRiver(riverCard);

    //set dealer
    Dealer dealer = new(new Deck(), table);

    // finish
    Game gameInstance = Game.GetInstance();
    gameInstance.FinishGame(dealer, players);
    RealPlayer winner = (RealPlayer)gameInstance.LastWinners[0]; 

    Assert.Equal(players[0].hand?.Score, winner.Score);
    Assert.Equal(players[0].hand?.Score, gameInstance.LastScore);
    Assert.Equal([players[0]], gameInstance.LastWinners);
  }
  [Fact]
  public void ShouldFinishTheGame_SplitedPot()
  {
    //set players
    List<Player> players = [
      new RealPlayer(BADASS_DRUMMER_NAME_FOR_FAKE_PLAYER),
      new FakePlayer(BADASS_GUITARIST_NAME_FOR_FAKE_PLAYER)
    ];
        List<Card> marioCards = HandHelper.GetCardsByNotation(HandHelper.SeparateCardsNotations("2h3d"));
        List<Card> johnCards = HandHelper.GetCardsByNotation(HandHelper.SeparateCardsNotations("2s3c"));
    players[0].ReceiveCard(marioCards[0]);
    players[0].ReceiveCard(marioCards[1]);
    players[1].ReceiveCard(johnCards[0]);
    players[1].ReceiveCard(johnCards[1]);

    //set table
    Table table = new();
    List<Card> flopCards = HandHelper.GetCardsByNotation(HandHelper.SeparateCardsNotations("AhAsAd"));
    Card turnCard = new("Ace", "Clubs");
    Card riverCard = new("5", "Clubs");
    table.ReceiveFlop(flopCards);
    table.ReceiveTurn(turnCard);
    table.ReceiveRiver(riverCard);

    //set dealer
    Dealer dealer = new(new Deck(), table);

    // finish
    Game gameInstance = Game.GetInstance();
    gameInstance.FinishGame(dealer, players);
    RealPlayer winner1 = (RealPlayer)gameInstance.LastWinners[0];

    Assert.Equal(players[0].hand?.Score, winner1.Score);
    Assert.Equal(players[0].hand?.Score, gameInstance.LastScore);
    Assert.Equal(players[1].hand?.Score, gameInstance.LastScore);
    Assert.Equal(players, gameInstance.LastWinners);
  }
}
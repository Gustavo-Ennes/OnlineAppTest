namespace TexasHoldemTests;

using TexasHoldem;

public class HandCheckTests
{
    readonly List<Card> playerCards = [
        new("Ace", "Diamonds"),
        new("6", "Clubs")
    ];

    readonly List<Card> tableCards = [
        new("King", "Spades"),
        new("Ace", "Clubs"),
        new("5", "Hearts"),
        new("3", "Spades"),
        new("Jack", "Diamonds")
    ];

    [Fact]
    public void ShouldIdentifyAHighCardGame()
    {
        List<Card> modifiedTableCards = [.. tableCards];
        modifiedTableCards.RemoveAt(1);
        modifiedTableCards.Insert(1, new Card("10", "Clubs"));
        string result = HandIdentifier.IdentifyPlayerHand(modifiedTableCards, playerCards);

        Assert.Equal("highCard", result);
    }
    [Fact]
    public void ShouldIdentifyAPair()
    {
        string result = HandIdentifier.IdentifyPlayerHand(playerCards, tableCards);
        Assert.Equal("pair", result);
    }
    [Fact]
    public void ShouldIdentifyTwoPairs()
    {
        List<Card> modifiedTableCards = [.. tableCards];
        modifiedTableCards.RemoveAt(0);
        modifiedTableCards.Insert(0, new Card("6", "Clubs"));
        string result = HandIdentifier.IdentifyPlayerHand(playerCards, modifiedTableCards);
        Assert.Equal("twoPair", result);
    }
    [Fact]
    public void ShouldIdentifyAThreeOfAKind()
    {
        List<Card> modifiedTableCards = [.. tableCards];
        modifiedTableCards.RemoveAt(0);
        modifiedTableCards.Insert(0, new Card("6", "Clubs"));
        modifiedTableCards.RemoveAt(1);
        modifiedTableCards.Insert(1, new Card("6", "Spades"));
        string result = HandIdentifier.IdentifyPlayerHand(playerCards, modifiedTableCards);
        Assert.Equal("threeOfAKind", result);
    }
    [Fact]
    public void ShouldIdentifyAStraight()
    {
        List<Card> modifiedTableCards = [.. tableCards];
        modifiedTableCards.RemoveAt(0);
        modifiedTableCards.Insert(0, new Card("2", "Clubs"));
        modifiedTableCards.RemoveAt(1);
        modifiedTableCards.Insert(1, new Card("4", "Spades"));
        string result = HandIdentifier.IdentifyPlayerHand(playerCards, modifiedTableCards);
        Assert.Equal("straight", result);
    }
    [Fact]
    public void ShouldIdentifyAFlush()
    {
        List<Card> modifiedTableCards = [.. tableCards];
        modifiedTableCards.RemoveAt(0);
        modifiedTableCards.Insert(0, new Card("2", "Diamonds"));
        modifiedTableCards.RemoveAt(1);
        modifiedTableCards.Insert(1, new Card("4", "Diamonds"));
        modifiedTableCards.RemoveAt(2);
        modifiedTableCards.Insert(2, new Card("Queen", "Diamonds"));
        string result = HandIdentifier.IdentifyPlayerHand(playerCards, modifiedTableCards);
        Assert.Equal("flush", result);
    }
    [Fact]
    public void ShouldIdentifyAFullHouse()
    {
        List<Card> modifiedTableCards = [.. tableCards];
        modifiedTableCards.RemoveAt(0);
        modifiedTableCards.Insert(0, new Card("6", "Diamonds"));
        modifiedTableCards.RemoveAt(2);
        modifiedTableCards.Insert(2, new Card("Ace", "Diamonds"));
        string result = HandIdentifier.IdentifyPlayerHand(playerCards, modifiedTableCards);
        Assert.Equal("fullHouse", result);
    }
    [Fact]
    public void ShouldIdentifyAFourOfAKind()
    {
        List<Card> modifiedTableCards = [.. tableCards];
        modifiedTableCards.RemoveAt(0);
        modifiedTableCards.Insert(0, new Card("Ace", "Diamonds"));
        modifiedTableCards.RemoveAt(2);
        modifiedTableCards.Insert(2, new Card("Ace", "Spades"));
        string result = HandIdentifier.IdentifyPlayerHand(playerCards, modifiedTableCards);
        Assert.Equal("fourOfAKind", result);
    }
    [Fact]
    public void ShouldIdentifyAStraightFlush()
    {
        List<Card> modifiedTableCards = [.. tableCards];
        modifiedTableCards.RemoveAt(0);
        modifiedTableCards.Insert(0, new Card("2", "Clubs"));
        modifiedTableCards.RemoveAt(1);
        modifiedTableCards.Insert(1, new Card("3", "Clubs"));
        modifiedTableCards.RemoveAt(2);
        modifiedTableCards.Insert(2, new Card("4", "Clubs"));
        modifiedTableCards.RemoveAt(3);
        modifiedTableCards.Insert(3, new Card("5", "Clubs"));
        string result = HandIdentifier.IdentifyPlayerHand(playerCards, modifiedTableCards);
        Assert.Equal("straightFlush", result);
    }
}
#pragma warning disable CS0436 // Type conflicts with imported type
namespace TexasHoldemTests;

using TexasHoldem;

public class PontuationTests
{
  // make sure that 9JQKA(highest highCard game) worth less than 
  // next category lowest score game (33224);
  [Fact]
  public void HighestCardGame_ShouldScoreLessThan_LowestPairGame()
  {
    // hand AKQJ9
    Hand highestScoreHighCardGame = HandHelper.GetHandByNotation("AhKdJdQc9s2s3h");
    // hand 22578
    Hand lowestScorePairGame = HandHelper.GetHandByNotation("2h2d3c4s5h7d8s");
    Assert.True(lowestScorePairGame.Score > highestScoreHighCardGame.Score);

  }
  [Fact]
  public void HighestCardGame_ShouldRespectHighCardRanking()
  {
    {
      // hand AKQJ9
      Hand highestScoreGame = HandHelper.GetHandByNotation("AhKdJdQc9s2s3h");
      // hand AKQJ8
      Hand lowerScoreGame = HandHelper.GetHandByNotation("AhKdJdQc8s2s3h");
      Assert.True(lowerScoreGame.Score < highestScoreGame.Score);
    }
  }
  [Fact]
  public void HighestPairGame_ShouldScoreLessThan_LowestTwoPairGame()
  {
    {
      // hand AAKQJ
      Hand highestPairGame = HandHelper.GetHandByNotation("AhAsKdJcQc2c3d");
      // hand 33226
      Hand lowestTwoPairGame = HandHelper.GetHandByNotation("3s3d2c2h4d5h6s");
      Assert.True(lowestTwoPairGame.Score > highestPairGame.Score);
    }
  }
  [Fact]
  public void HighestPairGame_ShouldRespectPairRanking()
  {
    {
      // hand AAKQJ
      Hand highestPairGame = HandHelper.GetHandByNotation("AhAsKdJcQc2c3d");
      // hand AAKQT
      Hand lowertPairGame = HandHelper.GetHandByNotation("AhAsKdJcTc2c3d");
      Assert.True(lowertPairGame.Score < highestPairGame.Score);
    }
  }
  [Fact]
  public void HighestTwoPairGame_ShouldScoreLessThan_LowestThreeOfAKindGame()
  {
    {
      // hand AAKKQ
      Hand highestTwoPairGame = HandHelper.GetHandByNotation("AhAsKdKcQc2c3d");
      // hand 22256
      Hand lowestThreeOfAKindGame = HandHelper.GetHandByNotation("2s2d2c3h4d5h6s");
      Assert.True(lowestThreeOfAKindGame.Score > highestTwoPairGame.Score);
    }
  }
  [Fact]
  public void HighestTwoPairGame_ShouldRespectTwoPairRanking()
  {
    {
      // hand AAKQJ
      Hand highestTwoPairGame = HandHelper.GetHandByNotation("AhAsKdJcQc2c3d");
      // hand AAKQT
      Hand lowerTwoPairGame = HandHelper.GetHandByNotation("AhAsKdJcTc2c3d");
      Assert.True(lowerTwoPairGame.Score < highestTwoPairGame.Score);
    }
  }
  [Fact]
  public void HighestThreeOfAKindrGame_ShouldScoreLessThan_LowestStraightGame()
  {
    {
      // hand AAAKQ
      Hand highestThreeOfAKindrGame = HandHelper.GetHandByNotation("AhAsAdKcQc2c3d");
      // hand A2345;
      Hand lowestStraightGame = HandHelper.GetHandByNotation("As2d3c4h5d7h8s"); 

      Assert.True(lowestStraightGame.Score > highestThreeOfAKindrGame.Score);
    }
  }
  [Fact]
  public void HighestThreeOfAKindGame_ShouldRespectThreeOfAKindRanking()
  {
    {
      // hand AAAKQ
      Hand highestThreeOfAKindGame = HandHelper.GetHandByNotation("AhAsAdKcQc2c3d");
      // hand AAAKJ
      Hand lowerThreeOfAKindGame = HandHelper.GetHandByNotation("AhAsAdKcJc2c3d");
      Assert.True(lowerThreeOfAKindGame.Score < highestThreeOfAKindGame.Score);
    }
  }
  [Fact]
  public void HighestStraightGame_ShouldScoreLessThan_LowestFlushGame()
  {
    {
      // hand AKQJT
      Hand highestThreeOfAKindrGame = HandHelper.GetHandByNotation("AhAsAdKcQc2c3d");
      // hand A2346(suited);
      Hand lowestStraightGame = HandHelper.GetHandByNotation("As2d3c4h5d7h8s");

      Assert.True(lowestStraightGame.Score > highestThreeOfAKindrGame.Score);
    }
  }
  [Fact]
  public void HighestStraightGame_ShouldRespectStraightRanking()
  {
    {
      // hand AKQJT
      Hand highestStraightGame = HandHelper.GetHandByNotation("AhQsKcJdTs2c3d");
      // hand KQJT9
      Hand lowerStraightGame = HandHelper.GetHandByNotation("QsKcJdTs9h2c3d");
      Assert.True(lowerStraightGame.Score < highestStraightGame.Score);
    }
  }
  [Fact]
  public void HighestFlushGame_ShouldScoreLessThan_LowestFullHouseGame()
  {
    {
      // hand AKQJ9(suited)
      Hand highestFlushGame = HandHelper.GetHandByNotation("AsKsQsJs9s2h3h");
      // hand 22233;
      Hand lowestFullHouseGame = HandHelper.GetHandByNotation("2d2c2h3s3d4c5h");

      Assert.True(lowestFullHouseGame.Score > highestFlushGame.Score);
    }
  }
  [Fact]
  public void HighestFlushGame_ShouldRespectFlushRanking()
  {
    {
      // hand AKQJ9(suited)
      Hand highestFlushGame = HandHelper.GetHandByNotation("AsKsQsJs9s2h3h");
      // hand AKQJ9(suited)
      Hand lowerFlushGame = HandHelper.GetHandByNotation("AsKsQsJs8s2h3h");
      Assert.True(lowerFlushGame.Score < highestFlushGame.Score);
    }
  }
  [Fact]
  public void HighestFullHouseGame_ShouldScoreLessThan_LowestFourOfAKindGame()
  {
    {
      // hand AAAKK
      Hand highestFullHouseGame = HandHelper.GetHandByNotation("AdAhAcKsKd2c3s");
      // hand 22223;
      Hand lowestFourOfAKindGame = HandHelper.GetHandByNotation("2d2c2h2s3s4d5c");

      Assert.True(lowestFourOfAKindGame.Score > highestFullHouseGame.Score);
    }
  }
  [Fact]
  public void HighestFullHouseGame_ShouldRespectFullHouseRanking()
  {
    {
      // hand AAAKK
      Hand highestFullHouseGame = HandHelper.GetHandByNotation("AdAhAcKsKd2c3s");
      // hand AAAQQ
      Hand lowerFullHouseGame = HandHelper.GetHandByNotation("AdAhAcQsQd2c3s");
      Assert.True(lowerFullHouseGame.Score < highestFullHouseGame.Score);
    }
  }
  [Fact]
  public void HighestFourOfAKindGame_ShouldScoreLessThan_LowestStraightFlushGame()
  {
    {
      // hand AAAAK
      Hand highestFourOfAKindGame = HandHelper.GetHandByNotation("AdAcAhAsKd2c3d");
      // hand A2345(suited);
      Hand lowestStraightFlushGame = HandHelper.GetHandByNotation("As2s3s4s5s7d8h");

      Assert.True(lowestStraightFlushGame.Score > highestFourOfAKindGame.Score);
    }
  }
  [Fact]
  public void HighestFourOfAKindGame_ShouldRespectFourOfAKindRanking()
  {
    {
      // hand AAAAK
      Hand highestFourOfAKindGame = HandHelper.GetHandByNotation("AdAcAhAsKd2c3d");
      // hand AAAAQ
      Hand lowerFourOfAKindGame = HandHelper.GetHandByNotation("AdAcAhAsQd2c3d");
      Assert.True(lowerFourOfAKindGame.Score < highestFourOfAKindGame.Score);
    }
  }
  [Fact]
  public void RoyalFlushGame_ShouldScoreMoreThan_HighestStraightFlushGame()
  {
    {
      // hand TJQKA(suited)
      Hand royalFlushGame = HandHelper.GetHandByNotation("AsKsQsJsTs2d3c");
      // hand 9TJQK(suited)
      Hand highestStraightFlushExceptRoyal = HandHelper.GetHandByNotation("KsQsJsTs9s2d3c");
      Assert.True(royalFlushGame.Score > highestStraightFlushExceptRoyal.Score);
    }
  }
}
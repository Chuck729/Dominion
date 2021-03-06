﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.0.0.0
//      SpecFlow Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace RHFYP_Test.BottomUpIntegration.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class LaboratoryBottomUpFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "LaboratoryBottomUp.feature"
#line hidden
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner(null, 0);
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "LaboratoryBottomUp", "\tBottom up integration testing of Laboratory card.", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public virtual void TestInitialize()
        {
            if (((testRunner.FeatureContext != null) 
                        && (testRunner.FeatureContext.FeatureInfo.Title != "LaboratoryBottomUp")))
            {
                RHFYP_Test.BottomUpIntegration.Features.LaboratoryBottomUpFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Create a new Laboratory card")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "LaboratoryBottomUp")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("bottomup")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("laboratory")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("unit")]
        public virtual void CreateANewLaboratoryCard()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create a new Laboratory card", new string[] {
                        "bottomup",
                        "laboratory",
                        "unit"});
#line 5
this.ScenarioSetup(scenarioInfo);
#line 6
 testRunner.Given("there are no cards", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 7
 testRunner.When("I create a Laboratory card", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 8
 testRunner.Then("a Laboratory card is created", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Add a new Laboratory card to a deck")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "LaboratoryBottomUp")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("bottomup")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("laboratory")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("deck")]
        public virtual void AddANewLaboratoryCardToADeck()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Add a new Laboratory card to a deck", new string[] {
                        "bottomup",
                        "laboratory",
                        "deck"});
#line 11
this.ScenarioSetup(scenarioInfo);
#line 12
 testRunner.Given("there is a deck with 0 Laboratory cards", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 13
 testRunner.And("there is a Laboratory card", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
 testRunner.When("I add the Laboratory card to the deck", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 15
 testRunner.Then("the deck contains 1 cards", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Draw an existing Laboratory card from a deck")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "LaboratoryBottomUp")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("bottomup")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("laboratory")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("deck")]
        public virtual void DrawAnExistingLaboratoryCardFromADeck()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Draw an existing Laboratory card from a deck", new string[] {
                        "bottomup",
                        "laboratory",
                        "deck"});
#line 18
this.ScenarioSetup(scenarioInfo);
#line 19
 testRunner.Given("there is a deck with 1 Laboratory cards", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 20
 testRunner.When("I draw a card from the deck", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 21
 testRunner.Then("the deck contains 0 cards", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 22
 testRunner.And("the drawn card is a Laboratory card", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("A player plays a Laboratory card")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "LaboratoryBottomUp")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("bottomup")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("laboratory")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("player")]
        public virtual void APlayerPlaysALaboratoryCard()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A player plays a Laboratory card", new string[] {
                        "bottomup",
                        "laboratory",
                        "player"});
#line 25
this.ScenarioSetup(scenarioInfo);
#line 26
 testRunner.Given("there is a player", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 27
 testRunner.And("the player has a Laboratory card in their hand", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 28
 testRunner.And("the player has 1 managers", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
 testRunner.And("the player is in the Action state", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 30
 testRunner.And("the player has 3 Laboratory cards in their hand", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
 testRunner.And("the player has 2 Laboratory cards in their draw pile", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 32
 testRunner.When("the player plays the Laboratory card", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 33
 testRunner.Then("the player has 1 managers", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 34
 testRunner.And("the player has 4 cards in their hand", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 35
 testRunner.And("the Laboratory card was played", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("A player plays a Laboratory card but doesn\'t have any managers")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "LaboratoryBottomUp")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("bottomup")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("laboratory")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("player")]
        public virtual void APlayerPlaysALaboratoryCardButDoesnTHaveAnyManagers()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A player plays a Laboratory card but doesn\'t have any managers", new string[] {
                        "bottomup",
                        "laboratory",
                        "player"});
#line 38
this.ScenarioSetup(scenarioInfo);
#line 39
 testRunner.Given("there is a player", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 40
 testRunner.And("the player has a Laboratory card in their hand", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
 testRunner.And("the player has 0 managers", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 42
 testRunner.And("the player is in the Action state", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 43
 testRunner.And("the player has 3 Laboratory cards in their hand", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 44
 testRunner.And("the player has 2 Laboratory cards in their draw pile", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 45
 testRunner.When("the player plays the Laboratory card", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 46
 testRunner.Then("the player has 0 managers", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 47
 testRunner.And("the player has 3 cards in their hand", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 48
 testRunner.And("the Laboratory card was not played", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("A player plays a Laboratory card but is not in the Action state")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "LaboratoryBottomUp")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("bottomup")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("laboratory")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("player")]
        public virtual void APlayerPlaysALaboratoryCardButIsNotInTheActionState()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("A player plays a Laboratory card but is not in the Action state", new string[] {
                        "bottomup",
                        "laboratory",
                        "player"});
#line 51
this.ScenarioSetup(scenarioInfo);
#line 52
 testRunner.Given("there is a player", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 53
 testRunner.And("the player has a Laboratory card in their hand", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 54
 testRunner.And("the player has 1 managers", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 55
 testRunner.And("the player is in the Buy state", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 56
 testRunner.And("the player has 3 Laboratory cards in their hand", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 57
 testRunner.And("the player has 2 Laboratory cards in their draw pile", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 58
 testRunner.When("the player plays the Laboratory card", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 59
 testRunner.Then("the player has 1 managers", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 60
 testRunner.And("the player has 3 cards in their hand", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 61
 testRunner.And("the Laboratory card was not played", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Try to add 1 player to a new game")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "LaboratoryBottomUp")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("bottomup")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("laboratory")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("game")]
        public virtual void TryToAdd1PlayerToANewGame()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Try to add 1 player to a new game", new string[] {
                        "bottomup",
                        "laboratory",
                        "game"});
#line 64
this.ScenarioSetup(scenarioInfo);
#line 65
 testRunner.Given("there is a game", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 66
 testRunner.When("I make the number of players in the game 1", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 67
 testRunner.Then("an ArgumentOutOfRangeException is thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Try to add five players to a new game")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "LaboratoryBottomUp")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("bottomup")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("laboratory")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("game")]
        public virtual void TryToAddFivePlayersToANewGame()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Try to add five players to a new game", new string[] {
                        "bottomup",
                        "laboratory",
                        "game"});
#line 70
this.ScenarioSetup(scenarioInfo);
#line 71
 testRunner.Given("there is a game", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 72
 testRunner.When("I make the number of players in the game 5", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 73
 testRunner.Then("an ArgumentOutOfRangeException is thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Buy a Laboratory card")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "LaboratoryBottomUp")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("bottomup")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("laboratory")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("game")]
        public virtual void BuyALaboratoryCard()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Buy a Laboratory card", new string[] {
                        "bottomup",
                        "laboratory",
                        "game"});
#line 76
this.ScenarioSetup(scenarioInfo);
#line 77
 testRunner.Given("there is a game", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 78
 testRunner.And("the game has 2 players altogether", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 79
 testRunner.And("player 0 has 6 golds", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 80
 testRunner.And("player 0 is in the Buy state", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 81
 testRunner.And("player 0 has 1 investments total", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 82
 testRunner.And("the game has 5 Laboratory cards for sale", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 83
 testRunner.When("player 0 buys a Laboratory card", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 84
 testRunner.Then("a Laboratory card is bought", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 85
 testRunner.And("player 0 has 1 gold total", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 86
 testRunner.And("player 0 has 0 investments total", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 87
 testRunner.And("the game has 4 Laboratory cards for sale", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion

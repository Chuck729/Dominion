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
namespace RHFYP_Test.Features.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.0.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class CIAPlayCardFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "CIAPlayCard.feature"
#line hidden
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner(null, 0);
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "CIAPlayCard", "\tAs a player, I want to be able to play a CIA card", ProgrammingLanguage.CSharp, ((string[])(null)));
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
                        && (testRunner.FeatureContext.FeatureInfo.Title != "CIAPlayCard")))
            {
                RHFYP_Test.Features.Features.CIAPlayCardFeature.FeatureSetup(null);
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
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Play a CIA card with two players, player\'s top card is discarded")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "CIAPlayCard")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("playcard")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("CIA")]
        public virtual void PlayACIACardWithTwoPlayersPlayerSTopCardIsDiscarded()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Play a CIA card with two players, player\'s top card is discarded", new string[] {
                        "playcard",
                        "CIA"});
#line 5
this.ScenarioSetup(scenarioInfo);
#line 6
 testRunner.Given("I have a game", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 7
 testRunner.And("the game has 2 players", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 8
 testRunner.And("player 0 has no cards", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 9
 testRunner.And("player 1 has no cards", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
 testRunner.And("player 0 is in Action mode", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 11
 testRunner.And("player 0 has 1 managers", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
 testRunner.And("player 1 has 5 non-action cards in their draw pile", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
 testRunner.And("player 0 has 5 non-action cards in their draw pile", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
 testRunner.And("player 0 has a CIA card in their hand", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
 testRunner.When("player 0 plays the CIA card and chooses to discard it", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 18
 testRunner.Then("player 0 has 4 non-action cards in their draw pile", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 19
 testRunner.And("player 1 has 4 non-action cards in their draw pile", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
 testRunner.And("player 0 has 1 non-action cards in their discard pile", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 21
 testRunner.And("player 1 has 1 non-action cards in their discard pile", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
 testRunner.And("player 0 has 1 non-action cards in their hand", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 23
 testRunner.And("player 0 does not have a CIA card in their hand", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
 testRunner.And("player 0 has a CIA card in their discard pile", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 25
 testRunner.And("player 0 has 1 managers", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Play CIA card no player given")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "CIAPlayCard")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("playcard")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("error")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("handling")]
        public virtual void PlayCIACardNoPlayerGiven()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Play CIA card no player given", new string[] {
                        "playcard",
                        "error",
                        "handling"});
#line 29
this.ScenarioSetup(scenarioInfo);
#line 30
 testRunner.Given("I have a game", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 31
 testRunner.And("there is a CIA card in the game", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 32
 testRunner.When("the CIA card is played without a player", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 33
 testRunner.Then("An ArgumentNullException is thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Play CIA card no game given")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "CIAPlayCard")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("playcard")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("error")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("handling")]
        public virtual void PlayCIACardNoGameGiven()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Play CIA card no game given", new string[] {
                        "playcard",
                        "error",
                        "handling"});
#line 36
this.ScenarioSetup(scenarioInfo);
#line 37
 testRunner.Given("I have a game", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 38
 testRunner.And("there is a CIA card in the game", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 39
 testRunner.When("the CIA card is played without a game", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 40
 testRunner.Then("An ArgumentNullException is thrown", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion

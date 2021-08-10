﻿using OpenQA.Selenium;
using Protractor;
using System;
using System.Threading;
using Xunit;
using Xunit.Gherkin.Quick;

/*
Feature: DMERKnownMedicalConditions.feature
    As a Driver Medical Fitness SME
    I want to confirm the known medical conditions business rules for a DMER

@pipeline
Scenario: Known Medical Conditions
    When I log in to the doctors' portal
    And I click on the DMER Forms tab
    And I click on the Case ID for 111
    And I refresh the page
    And I wait for the drivers licence field to have a value
    And I click on the Next button
    And the second page content is displayed
    And I click on the Next button
    And I enter the Uncorrected Binocular Vision as 20
    And I click on the Next button
    And I enter the known medical conditions
    And I click on the Next button
    And I enter the medical opinion and confirmations
    And I click on the Submit Form button
    Then I log out of the portal
*/

namespace bdd_tests
{
    [FeatureFile("./DMERKnownMedicalConditions.feature")]
    public sealed class DMERKnownMedicalConditions : TestBase
    {
    }
}
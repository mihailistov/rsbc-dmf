Feature: DMERCleanPass.feature
    As a Driver Medical Fitness SME
    I want to confirm the clean pass business rules for a DMER

Scenario: Non-Commercial DMER Supplemental O2 Not Driving Clean Pass
    When I log in to the doctors' portal
    And I click on the DMER link for the patient
    And the patient is a not commercial driver
    And binocular, corrected, or uncorrected vision is 20/50 or better
    And the acuity and visual field meets the criteria for license class
    And I select 'Yes' for Prescription of Supplemental O2
    And I select 'Yes' for Not While Driving
    And I submit the DMER form
    Then the DMER has a clean pass

Scenario: Commercial DMER Sleep Apnea, Mild, Hypopnea 0-14 Clean Pass
    When I log in to the doctors' portal
    And I click on the DMER link for the patient
    And the patient is a commercial driver
    And binocular, corrected, or uncorrected vision is 20/30 or better
    And the acuity and visual field meets the criteria for license class
    And I select 'Yes' for Diagnosis of Obstructive Sleep Apnea
    And I select 'Yes' for Mild
    And I select 'Yes' for Hypopnea Index 0-14
    And I submit the DMER form
    Then the DMER has a clean pass

Scenario: Non-Commercial DMER Sleep Apnea, Mild, Hypopnea 0-14 Clean Pass
    When I log in to the doctors' portal
    And I click on the DMER link for the patient
    And the patient is not a commercial driver
    And binocular, corrected, or uncorrected vision is 20/50 or better
    And the acuity and visual field meets the criteria for license class
    And I select 'Yes' for Diagnosis of Obstructive Sleep Apnea
    And I select 'Yes' for Mild
    And I select 'Yes' for Hypopnea Index 0-14
    And I submit the DMER form
    Then the DMER has a clean pass

Scenario: Commercial DMER Sleep Apnea, Mild, Epworth 0-10 Clean Pass
    When I log in to the doctors' portal
    And I click on the DMER link for the patient
    And the patient is a commercial driver
    And binocular, corrected, or uncorrected vision is 20/30 or better
    And the acuity and visual field meets the criteria for license class
    And I select 'Yes' for diagnosis of Obstructive Sleep Apnea
    And I select 'Yes' for Mild
    And I select 'Yes' for Epworth Score 0-10
    And I submit the DMER form
    Then the DMER has a clean pass

Scenario: Non-Commercial DMER Sleep Apnea, Mild, Epworth 0-10 Clean Pass
    When I log in to the doctors' portal
    And I click on the DMER link for the patient
    And the patient is not a commercial driver
    And binocular, corrected, or uncorrected vision is 20/50 or better
    And the acuity and visual field meets the criteria for license class
    And I select 'Yes' for diagnosis of Obstructive Sleep Apnea
    And I select 'Yes' for Mild
    And I select 'Yes' for Epworth Score 0-10
    And I submit the DMER form
    Then the DMER has a clean pass

Scenario: Commercial DMER Sleep Apnea, Moderate, Hypopnea 15-29 Clean Pass
    When I log in to the doctors' portal
    And I click on the DMER link for the patient
    And the patient is a commercial driver
    And binocular, corrected, or uncorrected vision is 20/30 or better
    And the acuity and visual field meets the criteria for license class
    And I select 'Yes' for Diagnosis of Obstructive Sleep Apnea
    And I select 'Yes' for Moderate
    And I select 'Yes' for Hypopnea Index 15-29
    And I submit the DMER form
    Then the DMER has a clean pass

Scenario: Non-Commercial DMER Sleep Apnea, Moderate, Hypopnea 15-29 Clean Pass
    When I log in to the doctors' portal
    And I click on the DMER link for the patient
    And the patient is not a commercial driver
    And binocular, corrected, or uncorrected vision is 20/50 or better
    And the acuity and visual field meets the criteria for license class
    And I select 'Yes' for Diagnosis of Obstructive Sleep Apnea
    And I select 'Yes' for Moderate
    And I select 'Yes' for Hypopnea Index 15-29
    And I submit the DMER form
    Then the DMER has a clean pass

Scenario: Commercial DMER Sleep Apnea, Moderate, Epworth 0-10 Clean Pass
    When I log in to the doctors' portal
    And I click on the DMER link for the patient
    And the patient is a commercial driver
    And binocular, corrected, or uncorrected vision is 20/30 or better
    And the acuity and visual field meets the criteria for license class
    And I select 'Yes' for diagnosis of Obstructive Sleep Apnea
    And I select 'Yes' for Moderate
    And I select 'Yes' for Epworth Score 0-10
    And I submit the DMER form
    Then the DMER has a clean pass

Scenario: Non-Commercial DMER Sleep Apnea, Moderate, Epworth 0-10 Clean Pass
    When I log in to the doctors' portal
    And I click on the DMER link for the patient
    And the patient is not a commercial driver
    And binocular, corrected, or uncorrected vision is 20/50 or better
    And the acuity and visual field meets the criteria for license class
    And I select 'Yes' for diagnosis of Obstructive Sleep Apnea
    And I select 'Yes' for Moderate
    And I select 'Yes' for Epworth Score 0-10
    And I submit the DMER form
    Then the DMER has a clean pass

Scenario: Commercial DMER Sleep Apnea, Severe, Hypopnea 30+ Clean Pass
    When I log in to the doctors' portal
    And I click on the DMER link for the patient
    And the patient is a commercial driver
    And binocular, corrected, or uncorrected vision is 20/30 or better
    And the acuity and visual field meets the criteria for license class
    And I select 'Yes' for Diagnosis of Obstructive Sleep Apnea
    And I select 'Yes' for Severe
    And I select 'Yes' for Hypopnea Index 30+
    And I submit the DMER form
    Then the DMER has a clean pass

Scenario: Non-Commercial DMER Sleep Apnea, Severe, Hypopnea 30+ Clean Pass
    When I log in to the doctors' portal
    And I click on the DMER link for the patient
    And the patient is not a commercial driver
    And binocular, corrected, or uncorrected vision is 20/50 or better
    And the acuity and visual field meets the criteria for license class
    And I select 'Yes' for Diagnosis of Obstructive Sleep Apnea
    And I select 'Yes' for Severe
    And I select 'Yes' for Hypopnea Index 30+
    And I submit the DMER form
    Then the DMER has a clean pass

Scenario: Commercial DMER Sleep Apnea, Severe, Epworth 0-10 Clean Pass
    When I log in to the doctors' portal
    And I click on the DMER link for the patient
    And the patient is a commercial driver
    And binocular, corrected, or uncorrected vision is 20/30 or better
    And the acuity and visual field meets the criteria for license class
    And I select 'Yes' for Diagnosis of Obstructive Sleep Apnea
    And I select 'Yes' for Severe
    And I select 'Yes' for Epworth Score 0-10
    And I submit the DMER form
    Then the DMER has a clean pass

Scenario: Non-Commercial DMER Sleep Apnea, Severe, Epworth 0-10 Clean Pass
    When I log in to the doctors' portal
    And I click on the DMER link for the patient
    And the patient is not a commercial driver
    And binocular, corrected, or uncorrected vision is 20/50 or better
    And the acuity and visual field meets the criteria for license class
    And I select 'Yes' for Diagnosis of Obstructive Sleep Apnea
    And I select 'Yes' for Severe
    And I select 'Yes' for Epworth Score 0-10
    And I submit the DMER form
    Then the DMER has a clean pass

Scenario: Commercial DMER Narcolepsy, No Daytime Attacks, Medical Compliance Clean Pass
    When I log in to the doctors' portal
    And I click on the DMER link for the patient
    And the patient is a commercial driver
    And binocular, corrected, or uncorrected vision is 20/30 or better
    And the acuity and visual field meets the criteria for license class
    And I select 'Yes' for Diagnosis of Narcolepsy
    And I select 'No' for Daytime Sleep Attacks or Cataplexy within the past 12 months
    And I select 'Yes' for Compliant with Medical Control Recommendations
    And I submit the DMER form
    Then the DMER has a clean pass

Scenario: Non-Commercial DMER Narcolepsy, No Daytime Attacks, Medical Compliance Clean Pass
    When I log in to the doctors' portal
    And I click on the DMER link for the patient
    And the patient is not a commercial driver
    And binocular, corrected, or uncorrected vision is 20/50 or better
    And the acuity and visual field meets the criteria for license class
    And I select 'Yes' for Diagnosis of Narcolepsy
    And I select 'No' for Daytime Sleep Attacks or Cataplexy within the past 12 months
    And I select 'Yes' for Compliant with Medical Control Recommendations
    And I submit the DMER form
    Then the DMER has a clean pass

Scenario: Commercial DMER Seizures, No Epilepsy Clean Pass
    When I log in to the doctors' portal
    And I click on the DMER link for the patient
    And the patient is a commercial driver
    And binocular, corrected, or uncorrected vision is 20/30 or better
    And the acuity and visual field meets the criteria for license class
    And I select 'Yes' for Seizures
    And I select 'No' for Diagnosis of Epilepsy
    And I submit the DMER form
    Then the DMER has a clean pass

Scenario: Non-Commercial DMER Seizures, No Epilepsy Clean Pass
    When I log in to the doctors' portal
    And I click on the DMER link for the patient
    And the patient is not a commercial driver
    And binocular, corrected, or uncorrected vision is 20/50 or better
    And the acuity and visual field meets the criteria for license class
    And I select 'Yes' for Seizures
    And I select 'No' for Diagnosis of Epilepsy
    And I submit the DMER form
    Then the DMER has a clean pass

Scenario: Commercial DMER Head Injury, Stable Clean Pass
    When I log in to the doctors' portal
    And I click on the DMER link for the patient
    And the patient is a commercial driver
    And binocular, corrected, or uncorrected vision is 20/30 or better
    And the acuity and visual field meets the criteria for license class
    And I select 'Yes' for Significant Head Injury
    And I select 'Yes' for Conditional Impairment and Considered Stable
    And I submit the DMER form
    Then the DMER has a clean pass

Scenario: Non-Commercial DMER Head Injury, Stable Clean Pass
    When I log in to the doctors' portal
    And I click on the DMER link for the patient
    And the patient is not a commercial driver
    And binocular, corrected, or uncorrected vision is 20/50 or better
    And the acuity and visual field meets the criteria for license class
    And I select 'Yes' for Significant Head Injury
    And I select 'Yes' for Conditional Impairment and Considered Stable
    And I submit the DMER form
    Then the DMER has a clean pass

Scenario: Commercial DMER Intracranial Tumours, Tumour Resected/Eliminated Clean Pass
    When I log in to the doctors' portal
    And I click on the DMER link for the patient
    And the patient is a commercial driver
    And binocular, corrected, or uncorrected vision is 20/30 or better
    And the acuity and visual field meets the criteria for license class
    And I select 'Yes' for Diagnosis of Intracranial Tumours
    And I select 'Yes' for Tumour Resected or Eliminated
    And I submit the DMER form
    Then the DMER has a clean pass

Scenario: Non-Commercial DMER Intracranial Tumours, Tumour Resected/Eliminated Clean Pass
    When I log in to the doctors' portal
    And I click on the DMER link for the patient
    And the patient is not a commercial driver
    And binocular, corrected, or uncorrected vision is 20/50 or better
    And the acuity and visual field meets the criteria for license class
    And I select 'Yes' for Diagnosis of Intracranial Tumours
    And I select 'Yes' for Tumour Resected or Eliminated
    And I submit the DMER form
    Then the DMER has a clean pass
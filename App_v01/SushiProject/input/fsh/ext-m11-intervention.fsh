//--------------------------------------------------------------------------------------
//
Extension: M11_Intervention
Id: m11-intervention
Description: """M11 Table of interventions"""
//Context: M11_ResearchStudyProfile
* insert rs-copyright-structure
* ^extension[$ext-fmm].valueInteger = 2
* ^extension[$ext-wg].valueCode = #brr
* ^status = #active
* value[x] 0..0

* . ^short = "Intervention details"
* . ^definition = "Intervention details that cannot be provided using ActivityDefinition."
* . ^comment = "This is very specificd to M11 Section 06."

//* obeys date-required

* extension contains
  armType 0..1 MS and
  interventionType 0..1 MS and  
  dosageStrength 0..1 and
  dosageLevels 0..1 and
  regimen 0..1 and
  use 0..1 and
  imp-nimp 0..1 and 
  sourcing 0..1

* extension[armType].value[x] only CodeableConcept
* extension[interventionType].value[x] only CodeableConcept
* extension[dosageStrength].value[x] only string
* extension[dosageLevels].value[x] only string
* extension[regimen].value[x] only string 
* extension[use].value[x] only CodeableConcept
* extension[imp-nimp].value[x] only CodeableConcept
* extension[sourcing].value[x] only CodeableConcept


* extension[armType].value[x] from m11-arm-type-vs
* extension[interventionType].value[x] from m11-intervention-type-vs
* extension[use].value[x] from m11-use-code-vs
* extension[imp-nimp].value[x] from m11-imp-nimp-type-vs
* extension[sourcing].value[x] from m11-source-code-vs

/* Invariant: date-required
Description: "At least one of approvalDate or signatureUrl SHOULD be populated."
Expression: "extension.where(url = 'approvalDate').exists() or extension.where(url = 'signatureUrl').exists()"
Severity: #warning */

//--------------------------------------------------------------------------------------
//
Profile: M11-Intervention-Table
Parent: ActivityDefinition
Id: m11-intervention-table
Description: """Profile of ActivityDefinition to be used as an Arm definition according to M11 Table of interventions """
* insert rs-copyright-structure
* ^extension[$ext-fmm].valueInteger = 2
* ^status = #active

* extension contains
  m11-intervention named intervention 0..* MS 



//--------------------------------------------------------------------------------------
// Value sets moved to M11Additional.fsh:
//   m11-use-code-vs (C217285), m11-imp-nimp-type-vs (C217286), m11-source-code-vs (C217052)
// Value sets not yet in TerminologyService (kept for reference — not moved):
//   m11-arm-type-vs (C217283), m11-intervention-type-vs (C217284)

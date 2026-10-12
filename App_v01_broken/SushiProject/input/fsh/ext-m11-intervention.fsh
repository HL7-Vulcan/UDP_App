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
//
ValueSet: M11ArmTypeVS
Id: m11-arm-type-vs
Title: "M11 Arm Type Elements Value Set using NCIT codes to create a FHIR value set."
Description: """Arm Type elements for describing trial interventions. This is an M11 specific value set.
Code List C217283: ICH OID 2.16.840.1.113883.3.989.2.3.1.15"""
* insert rs-copyright-terminology
* ^extension[$ext-fmm].valueInteger = 2
* ^experimental = false
* ^status = #active
* ^publisher = "ICH M11"

* $NCIT#C174267 "Active Comparator Arm"
* $NCIT#C174226 "Control Arm"
* $NCIT#C174266 "Experimental Arm"
* $NCIT#C174270 "No Intervention Arm"
* $NCIT#C174268 "Placebo Comparator Arm"
* $NCIT#C174269 "Sham Comparator Arm"


//--------------------------------------------------------------------------------------
//
ValueSet: M11InterventionTypeVS
Id: m11-intervention-type-vs
Title: "M11 Intervention Type Elements Value Set using NCIT codes to create a FHIR value set."
Description: """Intervention Type elements for describing trial interventions. This is an M11 specific value set.
- Code List C217284: ICH OID 2.16.840.1.113883.3.989.2.3.1.7"""
* insert rs-copyright-terminology
* ^extension[$ext-fmm].valueInteger = 2
* ^experimental = false
* ^status = #active
* ^publisher = "ICH M11" 

* $NCIT#C1909 "Drug" 
* $NCIT#C16830 "Medical Device" 
* $NCIT#C307 "Biologic" 
* $NCIT#C923 "Vaccine" 
* $NCIT#C218507 "Non Surgical Procedure" 
* $NCIT#C15329 "Surgery" 
* $NCIT#C15313 "Radiation" 
* $NCIT#C15184 "Behavioral" 
* $NCIT#C15238 "Genetic" 
* $NCIT#C1505 "Dietary Supplement" 
* $NCIT#C54696 "Combination Product" 
* $NCIT#C18020 "Diagnostic" 

//--------------------------------------------------------------------------------------
//
ValueSet: M11UseCodeVS
Id: m11-use-code-vs

Title: "M11 Use Code Elements Value Set using NCIT codes to create a FHIR value set."
Description: """Use Code elements for describing trial interventions. This is an M11 specific value set.
Code List C217285: ICH OID 2.16.840.1.113883.3.989.2.3.1.8"""
* insert rs-copyright-terminology
* ^extension[$ext-fmm].valueInteger = 2
* ^experimental = false
* ^status = #active
* ^publisher = "ICH M11" 

* $NCIT#C41161 "Experimental Intervention"
* $NCIT#C753 "Placebo"
* $NCIT#C165835 "Rescue Medicine" 
* $NCIT#C165822 "Background Treatment"
* $NCIT#C158128 "Challenge Agent" 
* $NCIT#C18020 "Diagnostic"
* $NCIT#C207614 "Additional Required Treatment"

//--------------------------------------------------------------------------------------
//
ValueSet: M11ImpNimpTypeVS
Id: m11-imp-nimp-type-vs
Title: "M11 imp-nimp Elements Value Set using NCIT codes to create a FHIR value set."
Description: """imp-nimp elements for describing trial interventions. This is an M11 specific value set.
- Code List C217286: ICH OID 2.16.840.1.113883.3.989.2.3.1.10"""
* insert rs-copyright-terminology
* ^extension[$ext-fmm].valueInteger = 2
* ^experimental = false
* ^status = #active
* ^publisher = "ICH M11" 

* $NCIT#C202579 "IMP"
* $NCIT#C156473 "NIMP"

//--------------------------------------------------------------------------------------
//
ValueSet: M11SourceCodeVS
Id: m11-source-code-vs
Title: "M11 Source Code Elements Value Set using NCIT codes to create a FHIR value set."
Description: """Source Code  elements for describing trial interventions. This is an M11 specific value set.
- Code List C217052: ICH OID 2.16.840.1.113883.3.989.2.3.1.9"""
* insert rs-copyright-terminology
* ^extension[$ext-fmm].valueInteger = 2
* ^experimental = false
* ^status = #active
* ^publisher = "ICH M11" 

* $NCIT#C215659 "Centrally Sourced"
* $NCIT#C215660 "Locally Sourced"

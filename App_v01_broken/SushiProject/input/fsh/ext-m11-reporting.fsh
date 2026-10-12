//--------------------------------------------------------------------------------------
//
Extension: M11_reporting
Id: m11-reporting
Description: """M11 AE Reporting Table"""
//Context: M11_ResearchStudyProfile
* insert rs-copyright-structure
* ^extension[$ext-fmm].valueInteger = 2
* ^extension[$ext-wg].valueCode = #brr
* ^status = #active

* value[x] 0..0
* . ^short = "reporting details"
* . ^definition = "reporting details."
* . ^comment = "This is very specific to M11 Section 09."

//* obeys date-required

* extension contains
  eventType 0..* MS and
  otherEventType 0..1 MS and  
  situationalScope 0..1 and
  reportablePeriodStart 0..1 and
  reportablePeriodEnd 0..1 and
  reportingTiming 0..1 and
  reportingMethod 0..1 and 
  backupReportingMethod 0..1

* extension[eventType].value[x] only CodeableConcept
* extension[otherEventType].value[x] only string  
* extension[situationalScope].value[x] only string
* extension[reportablePeriodStart].value[x] only date
* extension[reportablePeriodEnd].value[x] only date
* extension[reportingTiming].value[x] only string
* extension[reportingMethod].value[x] only string 
* extension[backupReportingMethod].value[x] only string

* extension[eventType].value[x] from m11-event-type-vs


/* Invariant: date-required
Description: "At least one of approvalDate or signatureUrl SHOULD be populated."
Expression: "extension.where(url = 'approvalDate').exists() or extension.where(url = 'signatureUrl').exists()"
Severity: #warning */

//--------------------------------------------------------------------------------------
//
Profile: M11-reporting-Table
Parent: Basic
Id: m11-reporting-table
Description: """Profile of Basic resource to be used as Reporting details  according to M11 Table of reportings """
* insert rs-copyright-structure
* ^extension[$ext-fmm].valueInteger = 2
* ^status = #active

* extension contains
  m11-reporting named reporting 0..* MS 



//--------------------------------------------------------------------------------------
//
ValueSet: M11EventTypeVS
Id: m11-event-type-vs
Title: "M11 Event Type Elements Value Set using NCIT codes to create a FHIR value set."
Description: """Event Type elements for describing trial reportings. This is an M11 specific value set.
Code List C217287: ICH OID 2.16.840.1.113883.3.989.2.3.1.22 """
* insert rs-copyright-terminology
* ^extension[$ext-fmm].valueInteger = 2
* ^experimental = false
* ^status = #active
* ^publisher = "ICH M11"

* $NCIT#C41331 "Adverse Event"
* $NCIT#C41335 "Serious Adverse Event"
* $NCIT#C218508 "Trial Intervention Complaint"
* $NCIT#C222331 "Drug/Device Combination Product Complaint"
* $NCIT#C25742 "Pregnancy Event"
* $NCIT#C218510 "Lactation Event"
* $NCIT#C218511 "Post-Partum Event"
* $NCIT#C218512 "Reportable Adverse Event of Special Interest"
* $NCIT#C218513 "Not Reportable Adverse Event of Special Interest"
* $NCIT#C17649 "Other" 


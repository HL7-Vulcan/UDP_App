Instance: TABLE_X
InstanceOf: m11-design-table
Title: "Protocol Design"
Usage: #example
Description: """Illustration of a Protocol Design as specified for Section 01 of the M11 Specification.

This desceribes an entirely ficticious protocol for an imaginary product with Brand name Exotex and generic name Exoticillin produced by Exemplar Pharmaceuticals.  The regulator is the Exemplar Regulating Authority (ERA) 
"""
* id = "{{TABLE_X}}"  // This is specified ONLY to allow documentatation of examples - it is not normally specified

* status = #active

//---------------------------------------------------------------
// Section 1.1.2	Overall Design

// C218675 (+ C97054) Intervention
* focus = Reference(Exemplar-MedicinalProduct)

// XC217277 C98746 Intervention Model
* extension[design].extension[interventionModel].valueCodeableConcept = {{C98746}}

// XC217278 C218703 Population Type
* extension[design].extension[populationType].valueCodeableConcept = {{C218703}}

// XC217279 C49647 Control Type
* extension[design].extension[controlType].valueCodeableConcept = {{C49647}}

// C112038 condition - see binding Population Diagnosis or Condition
* condition[+] = {{C112038}} 

// C97054 (Nonproprietary Name)  C142585 (INN) or not applicable M11 requires a narrative representation of the comparator - the name
// TODO Must give guidance on identifying INN
// TODO Must allow "not applicable"
* extension[design].extension[comparator][+].valueReference = Reference({{COMPARATOR_PRODUCT}})

// C49693 qty C50400 units Minimum Age value/units
// TODO Explain 2 fields in ICH being only 1 in FHIR
* extension[design].extension[minimumAge].valueQuantity = {{C49693}} // 18 $NCIT#C29848 "Years"

// C49694 qty C50400 units Maximum Age value/units
* extension[design].extension[maximumAge].valueQuantity = {{C49694}} // 55 $NCIT#C29848 "Years"

// XC217280 C218475 Intervention Assignment Method
* extension[design].extension[interventionAssignmentMethod].valueCodeableConcept = {{C218475}} //$NCIT#C25196 "Randomisation"

// C223137 Randomisation Type
* extension[design].extension[randomisationType].valueCodeableConcept.text = {{C223137}} //"block randomisation"

// C223138 Other Intervention Assignment Method (C223137 = Other)
* extension[design].extension[otherInterventionAssignmentMethod].valueString = {{CC223138}} 	//""

// C223136 Stratification Indicator
* extension[design].extension[stratificationIndicator].valueCodeableConcept = {{C223136}}  //$NCIT#C49488 "Yes"

// XC217049 C218704 Site Distribution
* extension[design].extension[siteDistribution].valueCodeableConcept = {{C218704}} //$NCIT#C217005 "Multicentre"

// XC217050 C218705 Site geographic scope
* extension[design].extension[siteGeographicScope].valueCodeableConcept = {{C218705}} //$NCIT#C217007 "Multiple Countries "

// XC217046 C218707 Master Protocol Indicator
* extension[design].extension[masterProtocolIndicator].valueCodeableConcept = {{C218707}} //$NCIT#C49487 "No"

// XC217046 C218708 Drug-Device Combination Product Indicator
* extension[design].extension[drug-DeviceCombinationProductIndicator].valueCodeableConcept = {{C218707}} //$NCIT#C49488 "Yes"

// XC217046 C218706 Adaptive Trial Design Indicator
* extension[design].extension[adaptiveTrialDesignIndicator].valueCodeableConcept = {{C218706}} //$NCIT#C49487 "No"

// C98771 Number of Arms
* extension[design].extension[numberOfArms].valueInteger = {{C98771}} //2

// XC217051 C49658 Trial Blind Schema
* extension[design].extension[trialBlindSchema].valueCodeableConcept = {{C49658}} //$NCIT#C187674 "Observer Blind"

// XC217281 C218709 Blinded Roles"
* extension[design].extension[blindedRoles][+].valueCodeableConcept = {{C218709}} //$NCIT#C142710 "Participant"  

// C218710 Target-Maximum"
* extension[design].extension[targetOrMaximum].valueCodeableConcept = {{C218710}} //$UDP_Target#T "Target"

// C49692 Number of Participants
* extension[design].extension[numberOfParticipants].valueInteger = {{C49692}} //200

// assigned vs enrolled

// C218712/C218713 total planned duration of trial intervention value/units
// * extension[design].extension[totalPlannedDurationOfTrialIntervention].valueQuantity =

// C218714 alternate description of planned duration of trial intervention if duration will vary
* extension[design].extension[alternateDescriptionOfPlannedDurationOfTrialIntervention].valueString = {{C218714}} //"""Will depend on response to treatment"""

// C218715/C218716 total planned duration of trial participation value/units
* extension[design].extension[totalPlannedDurationOfTrialParticipation].valueQuantity = {{C218715}} //3 $NCIT#C29846 "Months"

// C218717 alternate description of planned duration of trial participation if duration will vary
// * [alternateDescriptionOfPlannedDurationOfTrialParticipation].

// C218838 Additional Description of Duration
// * extension[design].extension[additionalDescriptionofDuration].

// XC217282 C218718 Independent Committees
* extension[design].extension[independentCommittees][+].valueCodeableConcept = {{C218718}} //$NCIT#C142578 "Independent Data Monitoring Committee"

// C218719 Other Committees
* extension[design].extension[otherCommittees].valueString = {{C218719}} //"Exemplar Monitoring Board"

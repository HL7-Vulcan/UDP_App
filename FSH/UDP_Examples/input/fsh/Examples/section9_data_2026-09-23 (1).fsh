Instance: Narrative-Composition-M11Section09
InstanceOf: m11-research-study-narratives
Title: "Example Narrative Single Composition with a Section for Each M11 Section"
Usage: #example
Description: """Example Narrative Single Composition with a contained section for e part of M11 Section 09.
"""

* status = #final
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"

/* 
* $NCIT#C218608  "ICH M11 Protocol Section 9 ADVERSE EVENTS, SERIOUS ADVERSE EVENTS, PRODUCT COMPLAINTS, PREGNANCY AND POSTPARTUM INFORMATION, AND SPECIAL SAFETY SITUATIONS"
* $NCIT#C218609  "ICH M11 Protocol Section 9.1 Definitions"
* $NCIT#C218610  "ICH M11 Protocol Section 9.1.1 Definitions of Adverse Events"
* $NCIT#C218611  "ICH M11 Protocol Section 9.1.2 Definitions of Serious Adverse Events"
* $NCIT#C218612  "ICH M11 Protocol Section 9.1.3 Definitions of Product Complaints"
* $NCIT#C218613  "ICH M11 Protocol Section 9.1.3.1 Definitions of Medical Device Product Complaints"
* $NCIT#C218614  "ICH M11 Protocol Section 9.2 Timing and Procedures for Collection and Reporting"
* $NCIT#C218615  "ICH M11 Protocol Section 9.2.1 Timing"
* $NCIT#C218616  "ICH M11 Protocol Section 9.2.2 Collection Procedures"

* $UDP_Term#013  "ICH M11 Protocol Section 9.2.2.1 Collection Procedures: Identification"
* $UDP_Term#014  "ICH M11 Protocol Section 9.2.2.2 Collection Procedures: Severity"
* $UDP_Term#015  "ICH M11 Protocol Section 9.2.2.3 Collection Procedures: Causality"
* $UDP_Term#016  "ICH M11 Protocol Section 9.2.2.4 Collection Procedures: Recording"
* $UDP_Term#017  "ICH M11 Protocol Section 9.2.2.5 Collection Procedures: Follow-up"

* $NCIT#C218617  "ICH M11 Protocol Section 9.2.3 Reporting"
* $NCIT#C218618  "ICH M11 Protocol Section 9.2.3.1 Regulatory Reporting Requirements"
* $NCIT#C218619  "ICH M11 Protocol Section 9.2.4 Adverse Events of Special Interest"
* $NCIT#C218620  "ICH M11 Protocol Section 9.2.5 Disease-related Events or Outcomes Not Qualifying as AEs or SAEs"
* $NCIT#C218621  "ICH M11 Protocol Section 9.3 Pregnancy and Postpartum Information"
* $NCIT#C218622  "ICH M11 Protocol Section 9.3.1 Participants Who Become Pregnant During the Trial"
* $NCIT#C218623  "ICH M11 Protocol Section 9.3.2 Participants Whose Partners Become Pregnant During the Trial"
* $NCIT#C218624  "ICH M11 Protocol Section 9.4 Special Safety Situations"


*/


* section[+]
  * title = "Section 9.1.1"
  * code = $NCIT#C218610  "ICH M11 Protocol Section 9.1.1 Definitions of Adverse Events"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
An adverse event (AE) is any untoward medical occurrence in a clinical trial participant administered a medicinal product, which does not necessarily have a causal relationship with this treatment.
        </div>
      """



* section[+]
  * title = "Section 9.1.2"
  * code = $NCIT#C218611  "ICH M11 Protocol Section 9.1.2 Definitions of Serious Adverse Events"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
A serious adverse event (SAE) is any untoward medical occurrence that results in death, is life-threatening, requires inpatient hospitalisation or prolongation of existing hospitalisation, results in persistent or significant disability or incapacity, or is a congenital anomaly or birth defect.
        </div>
      """


* section[+]
  * title = "Section 9.1.3"
  * code = $NCIT#C218612  "ICH M11 Protocol Section 9.1.3 Definitions of Product Complaints"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
A product complaint is any written, electronic, or oral communication that alleges deficiencies related to the identity, quality, durability, reliability, safety, effectiveness, or performance of a medicinal product.
        </div>
      """


* section[+]
  * title = "Section 9.1.3.1"
  * code = $NCIT#C218613  "ICH M11 Protocol Section 9.1.3.1 Definitions of Medical Device Product Complaints"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
A medical device product complaint is any written, electronic, or oral communication that alleges deficiencies related to the identity, quality, durability, reliability, safety, effectiveness, or performance of a medical device.
        </div>
      """


* section[+]
  * title = "Section 9.2"
  * code = $NCIT#C218614  "ICH M11 Protocol Section 9.2 Timing and Procedures for Collection and Reporting"
  * entry[+] = Reference(ReportingTable-Row1)
  * entry[+] = Reference(ReportingTable-Row2)
  * entry[+] = Reference(ReportingTable-Row3) 

* section[+]
  * title = "Section 9.2.1"
  * code = $NCIT#C218615  "ICH M11 Protocol Section 9.2.1 Timing"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
All adverse events will be collected from the time of informed consent until the end of the follow-up period as specified in the Schedule of Activities.
        </div>
      """


* section[+]
  * title = "Section 9.2.2.1"
  * code = $UDP_Term#013  "ICH M11 Protocol Section 9.2.2.1 Collection Procedures: Identification"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Adverse events will be identified through participant self-report, clinician observation, and review of clinical assessments at each visit.
        </div>
      """


* section[+]
  * title = "Section 9.2.2.2"
  * code = $UDP_Term#014  "ICH M11 Protocol Section 9.2.2.2 Collection Procedures: Severity"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
The severity of adverse events will be graded according to the Common Terminology Criteria for Adverse Events (CTCAE) version 5.0.
        </div>
      """


* section[+]
  * title = "Section 9.2.2.3"
  * code = $UDP_Term#015  "ICH M11 Protocol Section 9.2.2.3 Collection Procedures: Causality"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
The causal relationship between each adverse event and the investigational product will be assessed by the investigator as either related or not related.
        </div>
      """


* section[+]
  * title = "Section 9.2.2.4"
  * code = $UDP_Term#016  "ICH M11 Protocol Section 9.2.2.4 Collection Procedures: Recording"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{Cxxx}}
        </div>
      """


* section[+]
  * title = "Section 9.2.2.5"
  * code = $UDP_Term#017  "ICH M11 Protocol Section 9.2.2.5 Collection Procedures: Follow-up"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
All adverse events will be recorded in the electronic case report form within 24 hours of the investigator becoming aware of the event.
        </div>
      """

* section[+]
  * title = "Section 9.2.3"
  * code = $NCIT#C218617  "ICH M11 Protocol Section 9.2.3 Reporting"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Serious adverse events must be reported to the sponsor within 24 hours of the investigator becoming aware of the event.
        </div>
      """


* section[+]
  * title = "Section 9.2.3.1"
  * code = $NCIT#C218618  "ICH M11 Protocol Section 9.2.3.1 Regulatory Reporting Requirements"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
The sponsor will report all suspected unexpected serious adverse reactions (SUSARs) to the relevant regulatory authorities in accordance with local requirements.
        </div>
      """


* section[+]
  * title = "Section 9.2.4"
  * code = $NCIT#C218619  "ICH M11 Protocol Section 9.2.4 Adverse Events of Special Interest"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
The following adverse events have been identified as adverse events of special interest (AESI) for this trial based on the known safety profile of the investigational product.
        </div>
      """


* section[+]
  * title = "Section 9.2.5"
  * code = $NCIT#C218620  "ICH M11 Protocol Section 9.2.5 Disease-related Events or Outcomes Not Qualifying as AEs or SAEs"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{CxC21877xx}}
        </div>
      """


* section[+]
  * title = "Section 9.3.1"
  * code = $NCIT#C218622  "ICH M11 Protocol Section 9.3.1 Participants Who Become Pregnant During the Trial"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Female participants who become pregnant during the trial must be withdrawn from investigational product. Pregnancy outcomes will be followed up and reported.
        </div>
      """


* section[+]
  * title = "Section 9.3.2"
  * code = $NCIT#C218623  "ICH M11 Protocol Section 9.3.2 Participants Whose Partners Become Pregnant During the Trial"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
If a male participant's partner becomes pregnant during the trial or within 90 days of the last dose, the participant must notify the investigator who will report the pregnancy to the sponsor.
        </div>
      """


* section[+]
  * title = "Section 9.4"
  * code = $NCIT#C218624  "ICH M11 Protocol Section 9.4 Special Safety Situations"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Special safety situations include overdose, misuse, abuse, off-label use, medication errors, and occupational exposure. These must be reported to the sponsor within 24 hours.
        </div>
      """

//-------------------------------------------------------------------------------------
//
Instance: ReportingTable-Row1
InstanceOf: m11-reporting-table
Usage: #example

Description: "Table defining the trial reporting of adverse events etc"
* id = "ReportingTable-Row1" // This is specified ONLY to allow documentatation of examples - it is not normally specified

* code = http://terminology.hl7.org/CodeSystem/basic-resource-type#adminact

// TODO Work out how to distinguish Event Type and Other Event Type
* extension[reporting].extension[eventType][+].valueCodeableConcept = $NCIT#C41331 "Adverse Event"
* extension[reporting].extension[eventType][+].valueCodeableConcept = $NCIT#C41331 "Adverse Event"

* extension[reporting].extension[situationalScope].valueString = "All participants"
// TODO these should properly be the trial start date and end
* extension[reporting].extension[reportablePeriodStart].valueDate = "2024-01-15"
* extension[reporting].extension[reportablePeriodEnd].valueDate = "2024-07-15"
* extension[reporting].extension[reportingTiming].valueString = "Within 24 hours of awareness"
* extension[reporting].extension[reportingMethod].valueString = "Electronic data capture system" 
* extension[reporting].extension[backupReportingMethod].valueString = "Paper case report form"
//-------------------------------------------------------------------------------------
//
Instance: ReportingTable-Row2
InstanceOf: m11-reporting-table
Usage: #example

Description: "Table defining the trial reporting of adverse events etc"
* id = "ReportingTable-Row2" // This is specified ONLY to allow documentatation of examples - it is not normally specified

* code = http://terminology.hl7.org/CodeSystem/basic-resource-type#adminact

// TODO Work out how to distinguish Event Type and Other Event Type
* extension[reporting].extension[eventType][+].valueCodeableConcept = $NCIT#C41335 "Serious Adverse Event"
* extension[reporting].extension[eventType][+].valueCodeableConcept = $NCIT#C41335 "Serious Adverse Event"

* extension[reporting].extension[situationalScope].valueString = "All participants"
// TODO these should properly be the trial start date and end
* extension[reporting].extension[reportablePeriodStart].valueDate = "2024-01-15"
* extension[reporting].extension[reportablePeriodEnd].valueDate = "2024-07-15"
* extension[reporting].extension[reportingTiming].valueString = "Within 24 hours of awareness"
* extension[reporting].extension[reportingMethod].valueString = "Electronic data capture system" 
* extension[reporting].extension[backupReportingMethod].valueString = "Paper case report form"
//-------------------------------------------------------------------------------------
//
Instance: ReportingTable-Row3
InstanceOf: m11-reporting-table
Usage: #example

Description: "Table defining the trial reporting of adverse events etc"
* id = "ReportingTable-Row3" // This is specified ONLY to allow documentatation of examples - it is not normally specified

* code = http://terminology.hl7.org/CodeSystem/basic-resource-type#adminact

// TODO Work out how to distinguish Event Type and Other Event Type
* extension[reporting].extension[eventType][+].valueCodeableConcept = $NCIT#C25742 "Pregnancy Event"
* extension[reporting].extension[eventType][+].valueCodeableConcept = $NCIT#C25742 "Pregnancy Event"

* extension[reporting].extension[situationalScope].valueString = "Female participants and female partners of male participants"
// TODO these should properly be the trial start date and end
* extension[reporting].extension[reportablePeriodStart].valueDate = "2024-01-15"
* extension[reporting].extension[reportablePeriodEnd].valueDate = "2024-10-15"
* extension[reporting].extension[reportingTiming].valueString = "Within 5 business days of awareness"
* extension[reporting].extension[reportingMethod].valueString = "Electronic data capture system" 
* extension[reporting].extension[backupReportingMethod].valueString = "Paper case report form"
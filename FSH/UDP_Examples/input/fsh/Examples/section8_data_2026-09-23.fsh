Instance: Narrative-Composition-M11Section08
InstanceOf: m11-research-study-narratives
Title: "Example Narrative Single Composition with a Section for Each M11 Section"
Usage: #example
Description: """Example Narrative Single Composition with a contained section for e part of M11 Section 08.
"""

* status = #final
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"

/* 
* $NCIT#C218590  "ICH M11 Protocol Section 8 TRIAL ASSESSMENTS AND PROCEDURES"
* $NCIT#C218591  "ICH M11 Protocol Section 8.1 Trial Assessments and Procedures Considerations"
* $NCIT#C218592  "ICH M11 Protocol Section 8.2 Screening/Baseline Assessments and Procedures"

* $UDP_Term#011  "ICH M11 Protocol Section 8.2.1 Screening Assessments and Procedures"
* $UDP_Term#012  "ICH M11 Protocol Section 8.2.2 Baseline Assessments and Procedures"

* $NCIT#C218593  "ICH M11 Protocol Section 8.3 Efficacy Assessments and Procedures"
* $NCIT#C218594  "ICH M11 Protocol Section 8.4 Safety Assessments and Procedures"
* $NCIT#C218595  "ICH M11 Protocol Section 8.4.1 Physical Examination"
* $NCIT#C218596  "ICH M11 Protocol Section 8.4.2 Vital Signs"
* $NCIT#C218597  "ICH M11 Protocol Section 8.4.3 Electrocardiograms"
* $NCIT#C218598  "ICH M11 Protocol Section 8.4.4 Clinical Laboratory Assessments"
* $NCIT#C218599  "ICH M11 Protocol Section 8.4.5 Pregnancy Testing"
* $NCIT#C218600  "ICH M11 Protocol Section 8.4.6 Suicidal Ideation and Behaviour Risk Monitoring"
* $NCIT#C218601  "ICH M11 Protocol Section 8.5 Pharmacokinetics"
* $NCIT#C218602  "ICH M11 Protocol Section 8.6 Biomarkers"
* $NCIT#C218603  "ICH M11 Protocol Section 8.6.1 Genetics, Genomics, Pharmacogenetics and Pharmacogenomics"
* $NCIT#C218604  "ICH M11 Protocol Section 8.6.2 Pharmacodynamic Biomarkers"
* $NCIT#C218605  "ICH M11 Protocol Section 8.6.3 Other Biomarkers"
* $NCIT#C218606  "ICH M11 Protocol Section 8.7 Immunogenicity Assessments"
* $NCIT#C218607  "ICH M11 Protocol Section 8.8 Medical Resource Utilisation and Health Economics"
*/

* section[+]
  * title = "Section 8.1"
  * code = $NCIT#C218591  "ICH M11 Protocol Section 8.1 Trial Assessments and Procedures Considerations"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
All assessments and procedures will be performed in accordance with the Schedule of Activities. Any clinically significant findings will be followed up as appropriate.
        </div>
      """

* section[+]
  * title = "Section 8.2.1"
  * code = $UDP_Term#011  "ICH M11 Protocol Section 8.2.1 Screening Assessments and Procedures"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Screening assessments will include medical history, physical examination, vital signs, 12-lead ECG, and clinical laboratory tests to confirm eligibility.
        </div>
      """

* section[+]
  * title = "Section 8.2.2"
  * code = $UDP_Term#012  "ICH M11 Protocol Section 8.2.2 Baseline Assessments and Procedures"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Baseline assessments will be performed within 72 hours prior to the first dose of investigational product and will serve as the reference point for efficacy and safety evaluations.
        </div>
      """


* section[+]
  * title = "Section 8.3"
  * code = $NCIT#C218593  "ICH M11 Protocol Section 8.3 Efficacy Assessments and Procedures"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Efficacy will be assessed using the validated Primary Endpoint Scale at each scheduled visit. Secondary efficacy endpoints will also be assessed per the Schedule of Activities.
        </div>
      """


* section[+]
  * title = "Section 8.4"
  * code = $NCIT#C218594  "ICH M11 Protocol Section 8.4.2 Vital Signs"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Safety will be assessed through adverse event monitoring, clinical laboratory tests, vital signs measurements, physical examinations, and 12-lead ECGs.
        </div>
      """


* section[+]
  * title = "Section 8.4.1"
  * code = $NCIT#C218595  "ICH M11 Protocol Section 8.4.1 Physical Examination"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
A full physical examination will be performed at screening, and targeted physical examinations will be performed at subsequent visits as clinically indicated.
        </div>
      """


* section[+]
  * title = "Section 8.4.2"
  * code = $NCIT#C218596  "ICH M11 Protocol Section 8.4.2 Vital Signs"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Vital signs including blood pressure, heart rate, respiratory rate, and temperature will be measured at each visit after the participant has rested for at least 5 minutes.
        </div>
      """


* section[+]
  * title = "Section 8.4.3"
  * code = $NCIT#C218597  "ICH M11 Protocol Section 8.4.3 Electrocardiograms"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
A standard 12-lead ECG will be performed at screening, Day 1 (pre-dose), and at the end of treatment visit.
        </div>
      """


* section[+]
  * title = "Section 8.4.4"
  * code = $NCIT#C218598  "ICH M11 Protocol Section 8.4.4 Clinical Laboratory Assessments"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Clinical laboratory assessments including haematology, biochemistry, coagulation, and urinalysis will be performed at screening and at scheduled visits per the Schedule of Activities.
        </div>
      """


* section[+]
  * title = "Section 8.4.5"
  * code = $NCIT#C218599  "ICH M11 Protocol Section 8.4.5 Pregnancy Testing"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Pregnancy testing will be performed for women of childbearing potential at screening and throughout the trial per the Schedule of Activities.
        </div>
      """


* section[+]
  * title = "Section 8.4.6"
  * code = $NCIT#C218600  "ICH M11 Protocol Section 8.4.6 Suicidal Ideation and Behaviour Risk Monitoring"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Suicidal ideation and behaviour will be assessed using the Columbia Suicide Severity Rating Scale (C-SSRS) at each visit.
        </div>
      """


* section[+]
  * title = "Section 8.5"
  * code = $NCIT#C218601  "ICH M11 Protocol Section 8.5 Pharmacokinetics"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Blood samples for pharmacokinetic analysis will be collected on Day 1 and at Week 4 per the detailed PK sampling schedule.
        </div>
      """


* section[+]
  * title = "Section 8.6.1"
  * code = $NCIT#C218603  "ICH M11 Protocol Section 8.6.1 Genetics, Genomics, Pharmacogenetics and Pharmacogenomics"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
A single blood sample for pharmacogenomic analysis will be collected at baseline. Participation in pharmacogenomic sampling is optional.
        </div>
      """


* section[+]
  * title = "Section 8.6.2"
  * code = $NCIT#C218604  "ICH M11 Protocol Section 8.6.2 Pharmacodynamic Biomarkers"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Pharmacodynamic biomarker samples will be collected at screening, Day 1 (pre-dose), and Week 12.
        </div>
      """


* section[+]
  * title = "Section 8.6.3"
  * code = $NCIT#C218605  "ICH M11 Protocol Section 8.6.3 Other Biomarkers"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Other exploratory biomarker samples may be collected as specified in the Schedule of Activities and as described in the laboratory manual.
        </div>
      """


* section[+]
  * title = "Section 8.7"
  * code = $NCIT#C218606  "ICH M11 Protocol Section 8.7 Immunogenicity Assessments"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Anti-drug antibody samples will be collected at baseline and at the end of treatment visit.
        </div>
      """


* section[+]
  * title = "Section 8.8"
  * code = $NCIT#C218607  "ICH M11 Protocol Section 8.8 Medical Resource Utilisation and Health Economics"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
Healthcare resource utilisation data including hospitalisations and outpatient visits will be collected at each scheduled visit.
        </div>
      """

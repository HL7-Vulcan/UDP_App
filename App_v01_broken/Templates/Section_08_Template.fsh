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
{{C218769}}
        </div>
      """

* section[+]
  * title = "Section 8.2.1"
  * code = $UDP_Term#011  "ICH M11 Protocol Section 8.2.1 Screening Assessments and Procedures"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218770}}
        </div>
      """

* section[+]
  * title = "Section 8.2.2"
  * code = $UDP_Term#012  "ICH M11 Protocol Section 8.2.2 Baseline Assessments and Procedures"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218771}}
        </div>
      """


* section[+]
  * title = "Section 8.3"
  * code = $NCIT#C218593  "ICH M11 Protocol Section 8.3 Efficacy Assessments and Procedures"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218772}}
        </div>
      """


* section[+]
  * title = "Section 8.4"
  * code = $NCIT#C218594  "ICH M11 Protocol Section 8.4.2 Vital Signs"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218773}}
        </div>
      """


* section[+]
  * title = "Section 8.4.1"
  * code = $NCIT#C218595  "ICH M11 Protocol Section 8.4.1 Physical Examination"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C20989}}
        </div>
      """


* section[+]
  * title = "Section 8.4.2"
  * code = $NCIT#C218596  "ICH M11 Protocol Section 8.4.2 Vital Signs"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C154628}}
        </div>
      """


* section[+]
  * title = "Section 8.4.3"
  * code = $NCIT#C218597  "ICH M11 Protocol Section 8.4.3 Electrocardiograms"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C168186}}
        </div>
      """


* section[+]
  * title = "Section 8.4.4"
  * code = $NCIT#C218598  "ICH M11 Protocol Section 8.4.4 Clinical Laboratory Assessments"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218774}}
        </div>
      """


* section[+]
  * title = "Section 8.4.5"
  * code = $NCIT#C218599  "ICH M11 Protocol Section 8.4.5 Pregnancy Testing"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C92949}}
        </div>
      """


* section[+]
  * title = "Section 8.4.6"
  * code = $NCIT#C218600  "ICH M11 Protocol Section 8.4.6 Suicidal Ideation and Behaviour Risk Monitoring"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218775}}
        </div>
      """


* section[+]
  * title = "Section 8.5"
  * code = $NCIT#C218601  "ICH M11 Protocol Section 8.5 Pharmacokinetics"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218776}}
        </div>
      """


* section[+]
  * title = "Section 8.6.1"
  * code = $NCIT#C218603  "ICH M11 Protocol Section 8.6.1 Genetics, Genomics, Pharmacogenetics and Pharmacogenomics"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218777}}
        </div>
      """


* section[+]
  * title = "Section 8.6.2"
  * code = $NCIT#C218604  "ICH M11 Protocol Section 8.6.2 Pharmacodynamic Biomarkers"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218778}}
        </div>
      """


* section[+]
  * title = "Section 8.6.3"
  * code = $NCIT#C218605  "ICH M11 Protocol Section 8.6.3 Other Biomarkers"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218779}}
        </div>
      """


* section[+]
  * title = "Section 8.7"
  * code = $NCIT#C218606  "ICH M11 Protocol Section 8.7 Immunogenicity Assessments"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218780}}
        </div>
      """


* section[+]
  * title = "Section 8.8"
  * code = $NCIT#C218607  "ICH M11 Protocol Section 8.8 Medical Resource Utilisation and Health Economics"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C176849}}
        </div>
      """

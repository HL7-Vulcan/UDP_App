Instance: Narrative-Composition-M11Section11
InstanceOf: m11-research-study-narratives
Title: "Example Narrative Single Composition with a Section for Each M11 Section"
Usage: #example
Description: """Example Narrative Single Composition with a contained section for e part of M11 Section 11.
"""

* status = #final
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"

/* 
* $NCIT#C218649  "ICH M11 Protocol Section 11 TRIAL OVERSIGHT AND OTHER GENERAL CONSIDERATIONS"
* $NCIT#C218650  "ICH M11 Protocol Section 11.1 Regulatory and Ethical Considerations"
* $NCIT#C218651  "ICH M11 Protocol Section 11.2 Trial Oversight"
* $NCIT#C218652  "ICH M11 Protocol Section 11.2.1 Investigator Responsibilities"
* $NCIT#C218653  "ICH M11 Protocol Section 11.2.2 Sponsor Responsibilities"
* $NCIT#C218654  "ICH M11 Protocol Section 11.3 Informed Consent Process"
11.3.0.1 Description of Informed Consent Process
11.3.0.2 Description of Assent Process
11.3.0.3 Description of Emergency Consent Process
* $NCIT#C218655  "ICH M11 Protocol Section 11.3.1 Informed Consent for Rescreening"
* $NCIT#C218656  "ICH M11 Protocol Section 11.3.2 Informed Consent for Use of Remaining Samples in Exploratory Research"
* $NCIT#C218657  "ICH M11 Protocol Section 11.4 Committees"
* $NCIT#C218658  "ICH M11 Protocol Section 11.5 Insurance and Indemnity"
* $NCIT#C218659  "ICH M11 Protocol Section 11.6 Risk-Based Quality Management"
* $NCIT#C218660  "ICH M11 Protocol Section 11.7 Data Governance"
* $NCIT#C218661  "ICH M11 Protocol Section 11.8 Data Protection"
* $NCIT#C218662  "ICH M11 Protocol Section 11.9 Source Records"
11.9.0.2 Investigator Expectations for Source Records
11.9.0.3 Trial Monitor Expectations for Source Records
11.9.0.4 Identification of Source Records
* $NCIT#C218663  "ICH M11 Protocol Section 11.10 Protocol Deviations"
* $NCIT#C218664  "ICH M11 Protocol Section 11.11 Early Site Closure"
11.11.0.1 Decision Rights
11.11.0.2 Investigator Criteria
11.11.0.3 Responsibilities
* $NCIT#C218665  "ICH M11 Protocol Section 11.12 Data Dissemination"
*/

      
* section[+]
  * title = "Section 11.1"
  * code = $NCIT#C218650  "ICH M11 Protocol Section 11.1 Regulatory and Ethical Considerations"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218811}}
        </div>
      """
      
* section[+]
  * title = "Section 11.2"
  * code = $NCIT#C218651  "ICH M11 Protocol Section 11.2 Trial Oversight"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218812}}
        </div>
      """

* section[+]
  * title = "Section 11.2.1"
  * code = $NCIT#C218652  "ICH M11 Protocol Section 11.2.1 Investigator Responsibilities"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218813}}
        </div>
      """
      
* section[+]
  * title = "Section 11.2.2"
  * code = $NCIT#C218653  "ICH M11 Protocol Section 11.2.2 Sponsor Responsibilities"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218814}}
        </div>
      """
      
* section[+]
  * title = "Section 11.3"
  * code = $NCIT#C218654  "ICH M11 Protocol Section 11.3 Informed Consent Process"
  * section[+]
    * title = "Description of Informed Consent Process"
    * text.status = #additional    
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
  {{C184390}}
          </div>
        """
  * section[+]
    * title = "Description of Assent Process"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
  {{C218815}}
          </div>
        """
  * section[+]
    * title = "Description of Emergency Consent Process"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
  {{C218816}}
          </div>
        """

* section[+]
  * title = "Section 11.3.1"
  * code = $NCIT#C218655  "ICH M11 Protocol Section 11.3.1 Informed Consent for Rescreening"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218817}}
        </div>
      """
      
* section[+]
  * title = "Section 11.3.2"
  * code = $NCIT#C218656  "ICH M11 Protocol Section 11.3.2 Informed Consent for Use of Remaining Samples in Exploratory Research"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218818}}
        </div>
      """
      
* section[+]
  * title = "Section 11.4"
  * code = $NCIT#C218657  "ICH M11 Protocol Section 11.4 Committees"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218819}}
        </div>
      """
      
* section[+]
  * title = "Section 11.5"
  * code = $NCIT#C218658  "ICH M11 Protocol Section 11.5 Insurance and Indemnity"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218820}}
        </div>
      """
      
* section[+]
  * title = "Section 11.6"
  * code = $NCIT#C218659  "ICH M11 Protocol Section 11.6 Risk-Based Quality Management"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218821}}
        </div>
      """
      
* section[+]
  * title = "Section 11.7"
  * code = $NCIT#C218660  "ICH M11 Protocol Section 11.7 Data Governance"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218822}}
        </div>
      """
      
* section[+]
  * title = "Section 11.8"
  * code = $NCIT#C218661  "ICH M11 Protocol Section 11.8 Data Protection"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218823}}
        </div>
      """
      
* section[+]
  * title = "Section 11.9"
  * code = $NCIT#C218662  "ICH M11 Protocol Section 11.9 Source Records"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218824}}
        </div>
      """     
  * section[+]
    * title = "Investigator Expectations for Source Records"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
  {{C218825}}
          </div>
        """
  * section[+]
    * title = "Trial Monitor Expectations for Source Records"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
  {{C218826}}
          </div>
        """
  * section[+]
    * title = "Identification of Source Records"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
  {{C222675}}
          </div>
        """
 

* section[+]
  * title = "Section 11.10"
  * code = $NCIT#C218663  "ICH M11 Protocol Section 11.10 Protocol Deviations"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218827}}
        </div>
      """
      
* section[+]
  * title = "Section 11.11"
  * code = $NCIT#C218664  "ICH M11 Protocol Section 11.11 Early Site Closure"
  * section[+]
    * title = "Decision Rights"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
  {{C218828}}
          </div>
        """
  * section[+]
    * title = "Investigator Criteria"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
  {{C218829}}
          </div>
        """
  * section[+]
    * title = "Responsibilities"
    * text.status = #additional
    * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
  {{C218830}}
          </div>
        """
 

* section[+]
  * title = "Section 11.12"
  * code =  $NCIT#C218665  "ICH M11 Protocol Section 11.12 Data Dissemination"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218831}}
        </div>
      """
 
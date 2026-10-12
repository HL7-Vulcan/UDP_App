Instance: Narrative-Composition-M11Section06
InstanceOf: m11-research-study-narratives
Title: "Example Narrative Single Composition with a Section for Each M11 Section"
Usage: #example
Description: """Example Narrative Single Composition with a contained section for e part of M11 Section 06.
"""

* status = #final
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"

/* 
* $NCIT#C218560  "ICH M11 Protocol Section 6 TRIAL INTERVENTION AND CONCOMITANT THERAPY"
* $NCIT#C218561  "ICH M11 Protocol Section 6.1 Description of Investigational Trial Intervention"
* $NCIT#C218562  "ICH M11 Protocol Section 6.2 Rationale for Investigational Trial Intervention Dose and Regimen"
* $NCIT#C218563  "ICH M11 Protocol Section 6.3 Investigational Trial Intervention Administration"
* $NCIT#C218564  "ICH M11 Protocol Section 6.4 Investigational Trial Intervention Dose Modification"
* $NCIT#C218565  "ICH M11 Protocol Section 6.5 Management of Investigational Trial Intervention Overdose"
* $NCIT#C218566  "ICH M11 Protocol Section 6.6 Preparation, Storage, Handling and Accountability of Investigational Trial Intervention"
* $NCIT#C218567  "ICH M11 Protocol Section 6.6.1 Preparation of Investigational Trial Intervention"
* $NCIT#C218568  "ICH M11 Protocol Section 6.6.2 Storage and Handling of Investigational Trial Intervention"
* $NCIT#C218569  "ICH M11 Protocol Section 6.6.3 Accountability of Investigational Trial Intervention"
* $NCIT#C218570  "ICH M11 Protocol Section 6.7 Investigational Trial Intervention Assignment, Randomisation and Blinding"
* $NCIT#C218571  "ICH M11 Protocol Section 6.7.1 Participant Assignment to Investigational Trial Intervention"
* $NCIT#C218572  "ICH M11 Protocol Section 6.7.2 Randomisation"
* $NCIT#C218573  "ICH M11 Protocol Section 6.7.3 Measures to Maintain Blinding"
* $NCIT#C218574  "ICH M11 Protocol Section 6.7.4 Emergency Unblinding at the Site"
* $NCIT#C218575  "ICH M11 Protocol Section 6.8 Investigational Trial Intervention Adherence"
* $NCIT#C218576  "ICH M11 Protocol Section 6.9 Description of Noninvestigational Trial Intervention"
* $NCIT#C218577  "ICH M11 Protocol Section 6.9.1 Background Trial Intervention"
* $NCIT#C218578  "ICH M11 Protocol Section 6.9.2 Rescue Therapy"
* $NCIT#C218579  "ICH M11 Protocol Section 6.9.3 Other Noninvestigational Trial Intervention"
* $NCIT#C218580  "ICH M11 Protocol Section 6.10 Concomitant Therapy"
* $NCIT#C218581  "ICH M11 Protocol Section 6.10.1 Prohibited Concomitant Therapy"
* $NCIT#C218582  "ICH M11 Protocol Section 6.10.2 Permitted Concomitant Therapy"
*/

      
* section[+]
  * title = "Section 6"
  * code = $NCIT#C218560  "ICH M11 Protocol Section 6 TRIAL INTERVENTION AND CONCOMITANT THERAPY"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218747}}
        </div>
      """
{{REPEAT  * entry[+] = Reference({{TABLE_ROW_X}})}} 
      
* section[+]
  * title = "Section 6.1"
  * code = $NCIT#C218561  "ICH M11 Protocol Section 6.1 Description of Investigational Trial Intervention"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218751}}
        </div>
      """
      
* section[+]
  * title = "Section 6.2"
  * code = $NCIT#C218562  "ICH M11 Protocol Section 6.2 Rationale for Investigational Trial Intervention Dose and Regimen"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218752}}
        </div>
      """
      
* section[+]
  * title = "Section 6.3"
  * code = $NCIT#C218563  "ICH M11 Protocol Section 6.3 Investigational Trial Intervention Administration"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218753}}
        </div>
      """
      
* section[+]
  * title = "Section 6.4"
  * code = $NCIT#C218564  "ICH M11 Protocol Section 6.4 Investigational Trial Intervention Dose Modification"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218754}}
        </div>
      """
      
* section[+]
  * title = "Section 6.5"
  * code = $NCIT#C218565  "ICH M11 Protocol Section 6.5 Management of Investigational Trial Intervention Overdose"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218755}}
        </div>
      """
      
* section[+]
  * title = "Section 6.6.1"
  * code = $NCIT#C218567  "ICH M11 Protocol Section 6.6.1 Preparation of Investigational Trial Intervention"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C176274}}
        </div>
      """
      
* section[+]
  * title = "Section 6.6.2"
  * code = $NCIT#C218568  "ICH M11 Protocol Section 6.6.2 Storage and Handling of Investigational Trial Intervention"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C115525}}
        </div>
      """
      
* section[+]
  * title = "Section 6.6.3"
  * code = $NCIT#C218569  "ICH M11 Protocol Section 6.6.3 Accountability of Investigational Trial Intervention"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C176267}}
        </div>
      """
      
* section[+]
  * title = "Section 6.7.1"
  * code = $NCIT#C218571  "ICH M11 Protocol Section 6.7.1 Participant Assignment to Investigational Trial Intervention"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218756}}
        </div>
      """
      
* section[+]
  * title = "Section 6.7.2"
  * code = $NCIT#C218572  "ICH M11 Protocol Section 6.7.2 Randomisation"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C25196}}
        </div>
      """
      
* section[+]
  * title = "Section 6.7.3"
  * code = $NCIT#C218573  "ICH M11 Protocol Section 6.7.3 Measures to Maintain Blinding"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C189349}}
        </div>
      """
      
* section[+]
  * title = "Section 6.7.4"
  * code = $NCIT#C218574  "ICH M11 Protocol Section 6.7.4 Emergency Unblinding at the Site"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218757}}
        </div>
      """
      
* section[+]
  * title = "Section 6.8"
  * code = $NCIT#C218575  "ICH M11 Protocol Section 6.8 Investigational Trial Intervention Adherence"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218758}}
        </div>
      """
      
* section[+]
  * title = "Section 6.9"
  * code = $NCIT#C218576  "ICH M11 Protocol Section 6.9 Description of Noninvestigational Trial Intervention"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218759}}
        </div>
      """
      
* section[+]
  * title = "Section 6.9.1"
  * code = $NCIT#C218577  "ICH M11 Protocol Section 6.9.1 Background Trial Intervention"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C222329}}
        </div>
      """
      
* section[+]
  * title = "Section 6.9.2"
  * code = $NCIT#C218578  "ICH M11 Protocol Section 6.9.2 Rescue Therapy"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C222330}}
        </div>
      """
      
* section[+]
  * title = "Section 6.9.3"
  * code = $NCIT#C218579  "ICH M11 Protocol Section 6.9.3 Other Noninvestigational Trial Intervention"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218761}}
        </div>
      """
      
* section[+]
  * title = "Section 6.10"
  * code = $NCIT#C218580  "ICH M11 Protocol Section 6.10 Concomitant Therapy"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C53630}}
        </div>
      """
      
* section[+]
  * title = "Section 6.10.1"
  * code = $NCIT#C218581  "ICH M11 Protocol Section 6.10.1 Prohibited Concomitant Therapy"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218762}}
        </div>
      """
      
* section[+]
  * title = "Section 6.10.2"
  * code = $NCIT#C218582  "ICH M11 Protocol Section 6.10.2 Permitted Concomitant Therapy"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218763}}
        </div>
      """
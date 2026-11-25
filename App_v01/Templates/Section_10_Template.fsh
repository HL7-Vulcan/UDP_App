Instance: Narrative-Composition-M11Section10
InstanceOf: m11-research-study-narratives
Title: "Example Narrative Single Composition with a Section for Each M11 Section"
Usage: #example
Description: """Example Narrative Single Composition with a contained section for e part of M11 Section 10.
"""

* status = #final
* type = $NCIT#C207508 // Narrative
* date = "2025-06-30T12:46:00Z"
* author = Reference(Narrative-Organization) // Reference to Organization: Marketing 
* title = "Example Narrative with Section per Section - (this is the Composition Title}"

/* 
* $NCIT#C218625  "ICH M11 Protocol Section 10 STATISTICAL CONSIDERATIONS"
* $NCIT#C218626  "ICH M11 Protocol Section 10.1 General Considerations"
* $NCIT#C218627  "ICH M11 Protocol Section 10.2 Analysis Sets"
* $NCIT#C218628  "ICH M11 Protocol Section 10.3 Analyses of Demographics and Other Baseline Variables"
* $NCIT#C218629  "ICH M11 Protocol Section 10.4 Analyses Associated with the Primary Objective(s)"
* $NCIT#C218630  "ICH M11 Protocol Section 10.4.1 Primary Objective"
* $NCIT#C218631  "ICH M11 Protocol Section 10.4.1.1 Statistical Analysis Method"
* $NCIT#C218632  "ICH M11 Protocol Section 10.4.1.2 Handling of Data in Relation to Primary Estimand(s)"
* $NCIT#C218633  "ICH M11 Protocol Section 10.4.1.3 Handling of Missing Data in Relation to Primary Estimand(s)"
* $NCIT#C218634  "ICH M11 Protocol Section 10.4.1.4 Sensitivity Analysis"
* $NCIT#C218635  "ICH M11 Protocol Section 10.4.1.5 Supplementary Analysis"
* $NCIT#C218636  "ICH M11 Protocol Section 10.5 Analyses Associated with the Secondary Objective(s)"
* $NCIT#C218637  "ICH M11 Protocol Section 10.5.1 Secondary Objective"
* $NCIT#C218638  "ICH M11 Protocol Section 10.5.1.1 Statistical Analysis Method"
* $NCIT#C218639  "ICH M11 Protocol Section 10.5.1.2 Handling of Data in Relation to Secondary Estimand(s)"
* $NCIT#C218640  "ICH M11 Protocol Section 10.5.1.3 Handling of Missing Data in Relation to Secondary Estimand(s)"
* $NCIT#C218641  "ICH M11 Protocol Section 10.5.1.4 Sensitivity Analysis"
* $NCIT#C218642  "ICH M11 Protocol Section 10.5.1.5 Supplementary Analysis"
* $NCIT#C218643  "ICH M11 Protocol Section 10.6 Analyses Associated with the Exploratory Objective(s)"
* $NCIT#C218644  "ICH M11 Protocol Section 10.7 Safety Analyses"
* $NCIT#C218645  "ICH M11 Protocol Section 10.8 Other Analyses"
* $NCIT#C218646  "ICH M11 Protocol Section 10.9 Interim Analyses"
* $NCIT#C218647  "ICH M11 Protocol Section 10.10 Multiplicity Adjustments"
* $NCIT#C218648  "ICH M11 Protocol Section 10.11 Sample Size Determination"
*/

      
* section[+]
  * title = "Section 10.1"
  * code = $NCIT#C218626  "ICH M11 Protocol Section 10.1 General Considerations"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C164387}}
        </div>
      """
      
* section[+]
  * title = "Section 10.2"
  * code = $NCIT#C218627  "ICH M11 Protocol Section 10.2 Analysis Sets"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218801}}
        </div>
      """

* section[+]
  * title = "Section 10.3"
  * code = $NCIT#C218628  "ICH M11 Protocol Section 10.3 Analyses of Demographics and Other Baseline Variables"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218802}}
        </div>
      """

{{REPEAT

* section[+]
  * title = "Section 10.4.1.1"
  * code = $NCIT#C218631  "ICH M11 Protocol Section 10.4.1.1 Statistical Analysis Method"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218482}}
        </div>
      """
      
* section[+]
  * title = "Section 10.4.1.2"
  * code = $NCIT#C218632  "ICH M11 Protocol Section 10.4.1.2 Handling of Data in Relation to Primary Estimand(s)"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218803}}
        </div>
      """
      
* section[+]
  * title = "Section 10.4.1.3"
  * code = $NCIT#C218633  "ICH M11 Protocol Section 10.4.1.3 Handling of Missing Data in Relation to Primary Estimand(s)"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218804}}
        </div>
      """
      
* section[+]
  * title = "Section 10.4.1.4"
  * code = $NCIT#C218634  "ICH M11 Protocol Section 10.4.1.4 Sensitivity Analysis"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218480}}
        </div>
      """
      
* section[+]
  * title = "Section 10.4.1.5"
  * code = $NCIT#C218635  "ICH M11 Protocol Section 10.4.1.5 Supplementary Analysis"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218481}}
        </div>
      """

}}

{{REPEAT

* section[+]
  * title = "Section 10.5.1.1"
  * code = $NCIT#C218638  "ICH M11 Protocol Section 10.5.1.1 Statistical Analysis Method"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218482}}
        </div>
      """
      
* section[+]
  * title = "Section 10.5.1.2"
  * code = $NCIT#C218639  "ICH M11 Protocol Section 10.5.1.2 Handling of Data in Relation to Secondary Estimand(s)"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218805}}
        </div>
      """
      
* section[+]
  * title = "Section 10.5.1.3"
  * code = $NCIT#C218640  "ICH M11 Protocol Section 10.5.1.3 Handling of Missing Data in Relation to Secondary Estimand(s)"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218806}}
        </div>
      """
      
* section[+]
  * title = "Section 10.5.1.4"
  * code = $NCIT#C218641  "ICH M11 Protocol Section 10.5.1.4 Sensitivity Analysis"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218480}}
        </div>
      """
      
* section[+]
  * title = "Section 10.5.1.5"
  * code = $NCIT#C218642  "ICH M11 Protocol Section 10.5.1.5 Supplementary Analysis"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218481}}
        </div>
      """
      
}}

      
* section[+]
  * title = "Section 10.6"
  * code = $NCIT#C218643  "ICH M11 Protocol Section 10.6 Analyses Associated with the Exploratory Objective(s)"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218807}}
        </div>
      """
      
* section[+]
  * title = "Section 10.7"
  * code = $NCIT#C218644  "ICH M11 Protocol Section 10.7 Safety Analyses"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218808}}
        </div>
      """
      
* section[+]
  * title = "Section 10.8"
  * code = $NCIT#C218645  "ICH M11 Protocol Section 10.8 Other Analyses"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218809}}
        </div>
      """
      
* section[+]
  * title = "Section 10.9"
  * code = $NCIT#C218646  "ICH M11 Protocol Section 10.9 Interim Analyses"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C142582}}
        </div>
      """
      
* section[+]
  * title = "Section 10.10"
  * code = $NCIT#C218647  "ICH M11 Protocol Section 10.10 Multiplicity Adjustments"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C218810}}
        </div>
      """
      
* section[+]
  * title = "Section 10.11"
  * code = $NCIT#C218648  "ICH M11 Protocol Section 10.11 Sample Size Determination"
  * text.status = #additional
  * text.div = """<div xmlns='http://www.w3.org/1999/xhtml' xml:lang="en" lang="en"> 
{{C115467}}
        </div>
      """

Instance: InvestigationalMedicinalProduct
InstanceOf: {{MedicinalProductDefinition}}
Title: "Exemplar Medicinal Product"
Usage: #example
Description: """Illustration of a MedicinalProductDefinition used by the protocol
"""

* identifier[+].type.coding[+] = $NCIT#C218675 "Sponsor's Investigational Product Code"
* identifier[=].system = $SpID
* identifier[=].value = "{{C218675}}" //"EX2015/03"

* name[+].productName = "{{C97054}}" //"exoticillin 100micrograms/dose dry powder inhaler"
* name[=].type = $NCIT#C97054 "Nonproprietary Name(s)"
* name[+].productName = "{{C71898}}" //"Exotex 100 Inhaler"
* name[=].type = $NCIT#C71898 "Proprietary Name(s)"

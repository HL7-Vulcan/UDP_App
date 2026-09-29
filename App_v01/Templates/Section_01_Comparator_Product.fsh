Instance: {{COMPARATOR_PRODUCT}}
InstanceOf: MedicinalProductDefinition
Title: "Exemplar Comparator Product"
Usage: #example
Description: """Illustration of a MedicinalProductDefinition used by the protocol for the Comparator Product

C97054 (Nonproprietary Name)  C142585 (INN) or not applicable 
"""

// TODO Check Comparator product code type
* identifier[+].type.coding[+] = $NCIT#C218675 "Sponsor's Investigational Product Code"
* identifier[=].system = $SpID
* identifier[=].value = {{C218675}} // " EX2010/12"

* name[+].productName = "{{C97054}}" // "exoticillin 10micrograms/1.5ml intramuscular injection"
// TODO Must give guidance on identifying INN
* name[=].type = $NCIT#C97054 "Nonproprietary Name(s)"

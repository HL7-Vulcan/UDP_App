//--------------------------------------------------------------------------------------
//
Instance: {{TABLE_ROW_X}}
InstanceOf: m11-intervention-table
Usage: #example

Description: "Table defining the trial interventions"
* id = "{{TABLE_ROW_X}}" // This is specified ONLY to allow documentatation of examples - it is not normally specified

* status = #draft


* title = "{{C93729}}" // armName
* extension[intervention].extension[armType].valueCodeableConcept = "{{C172457}}"  // armType
* productCodeableConcept = $SpID#EX2015/03  "{{C177930}}" // Sponsor Investigational Product Code (and name)
* extension[intervention].extension[interventionType].valueCodeableConcept = "{{C98747}}"
* extension[intervention].extension[dosageStrength].valueString = "{{C142517}}"
* extension[intervention].extension[dosageLevels].valueString = "{{C94394}}"
* dosage[+].route = "{{C38114}}"
* extension[intervention].extension[regimen].valueString = "{{C15697}}"
* extension[intervention].extension[use].valueCodeableConcept = "{{C218748}}"
* extension[intervention].extension[imp-nimp].valueCodeableConcept = "{{C218749}}"
* extension[intervention].extension[sourcing].valueCodeableConcept = "{{C218750}}"


// TODO the following references a Medication but it should be a MedicationDefinition - FSH does not allow this even though it is in the spec
//* productReference = Reference(Medication-06)  // Nonproprietary Name // pharmaceuticalDoseForm

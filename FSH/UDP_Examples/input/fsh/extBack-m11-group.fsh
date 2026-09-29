//--------------------------------------------------------------------------------------
//
Profile: M11-R5-group
Parent: Group
Id: m11-r5-group
Description: """Profile of FHIR R5 Group resource to be used for backport of FHIR R6 Group for M11"""
* insert rs-copyright-structure
* ^extension[$ext-fmm].valueInteger = 2
* ^status = #active

* extension contains
  m11-r5-group-ext named r5-group 1..1 MS 

* characteristic.extension contains
  m11-r5-groupCharacteristic-ext named characteristic 0..1 SU


//--------------------------------------------------------------------------------------
//
Extension: M11-R5-Group-Extension
Id:  m11-r5-group-ext
Title:  "M11 Extension of FHIR R5 Group for use as backport of r6 group TO r5"
Description: """Add attributes present in R6 Group that are not in R5

title
status
combinationMethod

"""
* insert rs-copyright-structure
* ^extension[$ext-fmm].valueInteger = 2
* ^status = #active

* extension contains
  title 0..1 SU and
  status 0..1 SU ?! and
  combinationMethod 0..1 SU ?! 

* extension[title].value[x] only string

* extension[status].value[x] only code
* extension[status].value[x] from http://hl7.org/fhir/ValueSet/publication-status

* extension[combinationMethod].value[x] only code
* extension[combinationMethod].value[x] from http://hl7.org/fhir/ValueSet/group-characteristic-combination 


//--------------------------------------------------------------------------------------
//
Extension: M11-R5-GroupCharacteristic-Extension
Id:  m11-r5-groupCharacteristic-ext
Title:  "M11 Extension of FHIR R5 Group Characteristic for use as backport of r6 group TO r5"
Description: """Add attributes present in R6 Group that are not in R5

characteristic
  description

"""
* insert rs-copyright-structure
* ^extension[$ext-fmm].valueInteger = 2
* ^status = #active

* extension contains
  description 0..* SU

* extension[description].value[x] only string


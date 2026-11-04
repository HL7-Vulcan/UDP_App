// UDP Static Text Value Sets
// Value sets defined by the Vulcan UDP project that are not drawn from NCI or other
// standard terminologies — they use project-specific codes for fixed-choice fields.

//-----------------------------------------------------
ValueSet: UDPTarget-MaximumVS
Id: udp-target-Maximum-vs
Title: "UDP Target / Maximum"
Description: """Indicates whether a participant count is a target or a maximum.
This value set is defined by the Vulcan UDP project and is not an ICH M11 code list."""
* insert rs-copyright-terminology
* ^extension[$ext-fmm].valueInteger = 0
* ^experimental = false
* ^status = #active
* ^publisher = "Vulcan UDP"
* $UDP_Target#1 "Target"
* $UDP_Target#2 "Maximum"

# RandomBundles
Randomize community center bundles.

Select type in Advanced Settings in Character Creation.

# Experimental Commands
Use in SMAPI console, commands may cause permanent save damage.

setBundleType <S:type> <B:seeded>
  - type: 'normal', 'remixed', 'randomized'
  - seeded: true, false

Description: Sets bundle type, may lose bundle progress or submitted items.
  
  
bundle <I:ID> <B:completed>
  - ID: bundle id (or range e.g. 1-9)
  - seeded: true, false

Description: Sets bundle completion status, may lose submitted bundle items.

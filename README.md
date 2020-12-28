# RandomBundles
Randomize community center bundles.

Select type in Advanced Settings in Character Creation.

# Experimental Commands
Use in SMAPI console, commands may cause permanent save damage.

setBundleType <S:type> <B:seeded>
  - type: 'normal', 'remixed', 'randomized'
  - seeded: true, false

Description: Sets levels bundle type, may lose progress.
  
  
bundle <I:ID> <B:completed>
  - ID: bundle id 0-35
  - seeded: true, false

Description: Sets a bundles completion status, may lose bundle's items.

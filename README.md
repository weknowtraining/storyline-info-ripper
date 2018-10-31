# Description
This software will read the notes within a storyline (.story) file and allow you to generate a Notes Report or Narration Reports for each character. Output will be a formatted Word doc (.docx).

# Features
* Generate Notes report; A single document containing all of the notes within a .story file
* Generate Narration reports for each tagged narrator
* Option to include context lines for narrators
* Remove comments within notes

# Syntax
This program assumes the user is using a very basic, but consistent syntax for writing out their scripts within the notes sections.

Each line should follow this format:
```
CHARACTER: Scripted text.
```

Characters must be defined with all capital letters followed by a colon, then a space. As well, character declarations must start on their own line, the below text will not work. **KEVIN's text will be added to KOLTON's lines** but JOEY's text will work as expected.
```
KOLTON: This will not work at all. KEVIN: Damn, that sucks.
JOEY: Cmon Kevin, read the instructions!
```

The text following a character declaration **can** span multiple lines, and can include bullet points and lists. This will work:
```
KOLTON: It's a good thing I can write this on multiple lines.
I really have a lot to say! Look at all these things:
- Item 1
- Item 2
```

All text inside curly braces will be completely ignored when generating narration reports. This is useful for adding production notes, or comments to yourself.
```
KEVIN: I really hate {make his face angry when he says hate} it when Storyline crashes!
```

All other text will show up as written, including square brackets and parenthesis. ```[ ] ( )```

# Other Info

## This project uses the following third-party NuGet packages:
DotNetZip by Henrik/Dino Chisa : https://github.com/haf/DotNetZip.Semverd

DocX by Xceed : https://github.com/xceedsoftware/docx

Please refer to these packages for their respective licenses.

## Disclaimer
This software has not been thoroughly tested and may cause corruption of accessed files, use at your own risk.

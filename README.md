# Polymorph Plugin

This unofficial TaleSpire plugin is for adding dedicated transformations to the mini radial menu.
CMP adds a Transformation | Effect entry to the mini radial menu but specifically does not include
a entry for transforming minis because such an action will be used very infrequently. However, this
is not true for spell caster with spell like Polymorph or Druids using the wild shape ability. This
plugin allows you to add up to 10 transformations to the mini's radial menu.

![Preview](https://i.imgur.com/iidP1fD.png)

## Change Log

1.1.0: Polymorph is only available to GM and mini owner

1.1.0: Switched to using File Access Plugin

1.1.0: Bug fix for TS update. Moved Plolymorph into root folder.

1.0.0: Initial release

## Install

Install using R2ModMan. Expand thecontents of the TaleSpire_CustomData ZIP file into the Tale Spire
game directory. Merge the ZIP file contents with any other contents if the folder already exists.

## Usage

Transformations first need to be defined in R2ModMan. Open the R2ModMan configuration for the plugin
and add at least one transformation name. This is the same content name that would be used if the user
was using the CMP Transformation function. Ensure that the correesponding content exists in the Minis
folder. If it works using the CMP Transfromation function, it should would using this plugin.

Include only the content name in the configuration (do not include any paths or extensions).

Add an optional icon for each defined transformation. To add a icon, create a 32x32 PNG file with the
same name as the content and put it in:

```
D:\Steam\steamapps\common\TaleSpire\TaleSpire_CustomData\Images\Icons\org.lordashes.plugins.polymorph 
```

For example, to create a icon for the content 'Tiger' one would create a 32x32 PNG with the following
path and file name:

```
D:\Steam\steamapps\common\TaleSpire\TaleSpire_CustomData\Images\Icons\org.lordashes.plugins.polymorph\Tiger.PNG 
```

Once the plugin is loaded, right click a mini and select the Transformation icon. Then select the Polymorph
icon. This opens a sub-menu with all of the transformations defined in the R2ModMan configuration. If an icon
was not provided for some content, it gets the default Polymorph icon.

Select the desired transformation icon to transform the mini into that content.

## Limitations

Currently the R2ModMan configuration is a local configuration applied to all minis. As such each client
can have a different set of transformations defined and the results will still be correct on all clients.
However, it does mean that if a single client uses more than one transforming character, all of the transformations
will be available to all minis.

A future version will make the transformations character specific but for one: one set per client.

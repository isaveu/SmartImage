# SmartImage

*Find the source image in one click!*

SmartImage is a reverse image search tool for Windows with context menu integration. SmartImage will open the best match found returned from various image search engines (see the supported sites) right in your web browser. This behavior can be configured to the user's preferences.

Supported sites:

- SauceNao (`SauceNao`)
- ImgOps (`SauceNao`)
- Google Images (`GoogleImages`)
- TinEye (`TinEye`)
- IQDB (`Iqdb`)
- trace.moe (`TraceMoe`)
- KarmaDecay (`KarmaDecay`)

**[Latest standalone binary](https://github.com/Decimation/SmartImage/blob/master/SmartImage/bin/Release/netcoreapp3.0/win10-x64/publish/SmartImage.exe)**

# Example

![Demo](https://github.com/Decimation/SmartImage/raw/master/Demo.gif)

![Context menu image](https://github.com/Decimation/SmartImage/blob/master/Context%20menu%20integration.png)

# Configuration

SmartImage stores its configuration in registry. The rationale behind this is that SmartImage is designed to be used primarily through the context menu, so configuration persisting between uses is more logical.

# Commands

`--set-imgur-auth <consumer id> <consumer secret>`

Configures Imgur API keys. Register an application [here](https://api.imgur.com/oauth2/addclient), then get your ID [here](https://imgur.com/account/settings/apps). If this is configured, SmartImage will use Imgur to upload temporary images instead of ImgOps.

`--search-engines <engines>`

Sets the search engines to use when searching, delimited by commas.

`--priority-engines <engines>`

Sets the priority search engines. Priority search engines are engines whose results will be automatically opened in your browser when searching is complete. *For example, if you designate SauceNao as a priority engine, then results returned by
SauceNao will be automatically opened in your browser.*

`--ctx-menu`

Installs context menu integration.

# Notes

- Ensure that the executable is placed in the system PATH (*%PATH%*), otherwise the context menu integration will not work.
- SmartImage uploads temporary images using ImgOps (the uploaded images are automatically deleted after 2 hours). Imgur can also be used, but you must register an Imgur application client.

# Inspiration

- [SauceNao-Windows](https://github.com/RoxasShadow/SauceNao-Windows)
- [SharpNao](https://github.com/Lazrius/SharpNao)
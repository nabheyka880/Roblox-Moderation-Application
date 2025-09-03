<div align="center">
<img src="https://upload.wikimedia.org/wikipedia/commons/thumb/7/70/Roblox_Corporation_2025_logo.svg/640px-Roblox_Corporation_2025_logo.svg.png" Height="300" align="center" />
</div>
  
# // ROBLOX Moderation Application //

<h3 align="center">
  <a href="https://github.com/nabheyka880/ROBLOX-Moderation-Application#how-can-i-make-a-good-github-issue">Issue posting</a>
  <span> - </span>
  <a href="https://github.com/nabheyka880/ROBLOX-Moderation-Application#how-does-this-work">Functionality</a>
  <span> - </span>
  <a href="https://github.com/nabheyka880/ROBLOX-Moderation-Application#an-error-occured-while-trying-to-____">Errors</a>
  <span> - </span>
  <a href="https://github.com/nabheyka880/ROBLOX-Moderation-Application#how-was-this-made">Workflows</a>
</h3>

*Moderate ROBLOX users into your game in a modern and fast way.*

The *ROBLOX Moderation Application* is a Windows application to ban ROBLOX rule breakers from your game in a easy way.

**This application is ONLY working for Windows, and is not made in any way from ROBLOX. It's a tool made by NabHeyka. This tool does not have plans for Mobile support.**

# How can i make a good GitHub issue?
If you're having issue with the application and would like to ask for support, you can open an issue here on the GitHub repo.
However, there are some things you need to do to make your issue readable and comprehensible:
- **DON'T PUT THE ERROR IN THE TITLE** -- Instead, put a simple title that describes the issue you're having in simple words. Put the error in the description of the issue and make it readable enough for developers to read and understand it.
- **READ, SEARCH, THEN ASK** -- First of all, look for the issue you're having *mainly in the "An error occured while trying to ___" section*, THEN you can ask for support. Also, please look for any similiar posts to your issue. Perhaps there's a similiar one that talks about your issue and it's probably already solved!
- **DON'T ASK TO ASK** -- Like in this [site](https://dontasktoask.com), don't ask stuff like "any dev around here?" or "can anyone help me?" and nothing else. This is public for a reason. Just simply ask what you need help with, and wait for someone to reply.
- **USE IT CORRECTLY** -- Now, this sounds very confusing. In short words, if you installed a fork of this application or having issue with something not realted to this, do NOT ask here. Ask in their GitHub page, discord server, anywhere but not here. I do not care if they don't have a place a public place. Sorry *not sorry*.
- **BE COMPREHENSIBLE** -- Let people understand what you're saying. You will not be helped if no one understands what you're saying.
- **DON'T MISUSE** -- I mean, don't use the issues to promote your stuff or something. Why would you do that?

# How does this work?
This application uses ROBLOX's Open Cloud API for most of it's functionality, such as getting User's information and thumbnail, banning and unbanning the players and much more.

**This means that you will need a ROBLOX API Key to use this application. To get one, go to the [Creator Hub Dashboard](create.roblox.com/dashboard), on the left select *Open Cloud* and then *API Keys*. From there, create an API Key with the required information.**

Pro tip: When making a ROBLOX API key, make sure you give it these permissions:
- `universe.user-restriction:write`. This is to ban and unban the players;
- `user.advanced:read` and `user.social:read`. This is to access the player's information, such as the name and display namme, and thumbnail.
You can give the Key whatever permissions you want, these 3 though are required to use this application.

# An error occured while trying to ____
Sometimes, an error may occur in the Main page when typing in the player's user id. You will know if an error notification pops up while typing in the User ID. This is possible due to the fact that ROBLOX's Open Cloud API had an issue with either getting the user's profile information or the user's profile thumbnail. 

**Here's all the codes you will probably get in this case:**
- `Error code 400 / INVALID_ARGUMENT`: This happens when an invalid argument was passed in the HTTP request URL, such as an invalid universe_id or a missing header like Content-Type or Content-Length. In this case, open an issue here on the GitHub, detailing what error you're getting and when you're getting it.
- `Error code 403 / PERMISSION_DENIED`: This happens when your Key doesn't have the permissions to run a specific action, such as getting the user's information or the user's thumbnail, banning and unbanning the user. In this case, make sure that your key has the correct permissions. You can see what you need on the section above.
- `Error code 404 / NOT_FOUND`: This happens when the system can't find specified resources. This is most likely an issue that will not occur. But in case it does, open an issue on GitHub detailing the error you're getting and when you're getting it.
- `Error code 409 / ABORTED`: This happens when the requested action was aborted / cancelled. This is most likely an issue that will not occur.
- `Error code 429 / RESOURCE_EXHAUSTED`: This happens when there's not enough quota to perform this action. In terms more comprehensible, it happens when there are too many requests being sent. In this case, try waiting a few seconds before performing the action again.
- `Error code 499 / CANCELLED`: This happens when the system terminates the request, and is mostly caused due to a client side timeout. I'm not sure, but it's most likely an issue that will not occur.
- `Error code 500 / INTERNAL`: This happens when there was an interal server error, that is mostly caused due to a server bug. This is most likely a ROBLOX issue, and you're only solution is to probably wait until ROBLOX fixes this bug.
- `Error code 501 / NOT_IMPLEMENTED`: This happens when the server calls an API function that is not implemented. This is most likely an issue that will not occur, unless ROBLOX removes one of the used API calls in this application.
- `Error code 503 / UNAVAILABLE`: This happens when the service is unavailable, that is mostly caused due to the server being down. This is most likely an issue that will not occur. But if it does, it is most likely a ROBLOX issue, and the only solution is to wait until ROBLOX fixes this issue.

# How was this made?
You can check the Credits section in the Settings page for the workflows. But if you don't want to:
- [WPF UI](https://wpfui.lepo.co/)

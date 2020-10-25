# WindowRememberer

WindowRememberer is a simple system tray app for Windows that does two things:

- It lets you save and restore window sizes and positions. This comes in handy when a e.g. a game changes the resolution and all your windows are suddenly all screwed up.
- It has a bunch of global hotkeys for setting the window size and position in the same way as [Magnet](https://magnet.crowdcafe.com/). These are all copied from Magnet, but instead of Magnet's default modifiers (ctrl+alt), it uses shift+alt+win.
    - Note that Magnet is a very good, polished, commercial app. This is none of those things. It may crash and be buggy, but it fits my needs and hopefully may fit yours.

Developed and tested on Windows 10. May or may not work on your machine.

The code is quite likely non-idiomatic C#, to put it kindly.

The name may change. I couldn't think of anything better.

## TODO

- Save properties to disk.
- Restoring maximized windows properly.
- Multi-window support?
- Add the Magnet-ish window resize stuff to the system tray app menu.
- Preferences window to allow the user to choose their own modifiers.

## Acknowledgements (aka stuff I copied from)

- https://www.codeproject.com/articles/290013/formless-system-tray-application
- http://stackoverflow.com/a/6485074

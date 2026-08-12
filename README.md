# Video Poker — Jacks or Better

A single-player Jacks or Better video poker game for Android, built in Unity. Published on Google Play.

Draw five cards, hold the ones you want, draw again, and get paid according to the hand you end up with.
Around it: daily and weekly missions, an unlockable card-back shop, statistics, sound settings and in-app
purchases.

> **Play Store:** the published build lives on my
> [developer page](https://play.google.com/store/apps/dev?id=5968286876093646143).

---

## What is worth reading

The repository is a full Unity project, so most of it is scenes, sprites and imported packages. The parts I
actually wrote are under [`Assets/_SCRIPTS/`](Assets/_SCRIPTS) and [`Assets/Editor/`](Assets/Editor):

| File | Lines | What it does |
|---|---:|---|
| `Game/PokerHandEvaluator.cs` | 205 | Hand ranking. Checks strongest to weakest and returns the payout multiplier directly, so the paytable is the return value rather than a second lookup table that can drift out of sync |
| `Game/GameManagerVideoPoker.cs` | 363 | Round flow: deal, hold, draw, evaluate, pay, and the state that has to survive an interrupted round |
| `Kayit/KAYIT.cs` | 312 | Save system — credits, unlocked card backs, statistics, mission progress |
| `Fonksiyonlar/GOREV_YONETICISI.cs` | 157 | Daily and weekly missions: generation, progress tracking, reward claiming |
| `Game/KartDestesi.cs` | 75 | Deck: shuffle and draw without replacement |
| `IAP/IAPManager.cs` | 108 | In-app purchases |
| `Editor/GameBalanceWindow.cs` | 160 | A custom editor window — see below |

### The evaluator

`Evaluate()` walks the hands from Royal Flush down to Jacks or Better and returns the multiplier of the first
one that matches. One ordered pass, no scoring table to keep in step with the UI.

It also carries a comment about a bug that was there for a while: every check was writing its result into the
same variable, so Royal Flush could never fire. The comment stays in the file, because a fixed bug is worth
more as a note than as silence.

### The editor window

`GameBalanceWindow.cs` adds a **Video Poker → Game Balance & Debug** menu inside Unity: set credits, force a
hand, reset missions, inspect saved state. Balancing a payout table by playing the game until the case you
want shows up does not work — a five-card draw will not hand you a Royal Flush on request. The tooling that
makes a game testable is part of building it.

---

## Building it

```
Unity 2022.3 LTS or newer, Android build support
```

Open the folder as a Unity project and let it import. It runs in the editor; the Android build additionally
needs a signing keystore, which is **not** in this repository — set your own under
*Project Settings → Player → Publishing Settings*.

## Layout

```
Assets/_SCRIPTS/      the game: cards, rounds, saving, missions, IAP, UI
Assets/Editor/        the balance and debug window
Assets/TextMesh Pro/  imported package, not my code
ProjectSettings/      Unity project configuration
```

## Notes

Class and file names are Turkish (`KartDestesi` = deck, `KAYIT` = save, `GOREV` = mission) — the project was
written that way and renaming it now would only add noise to a shipped game's history.

The art, sound and the published build are mine and are not offered for reuse. The code is here to be read.

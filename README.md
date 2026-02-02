# MG4
## Devlog
### 1. Application of MVC Pattern
For this project, I structured the game around the MVC (Model-View-Controller) pattern to keep player logic separate from UI and audio systems, since the Model layer wasn’t relevant to the core gameplay goals, I focused solely on splitting the Controller (logic) and View (feedback) layers to avoid messy dependencies between code systems.


#### 1.1 Controller Layer: Handling Core Game Logic (No View Interactions)
The Controller layer is where all the "game rules" live with no UI updates, no sound effects, just pure logic. Here’s how I broke it down:
- **PlayerController.cs**: This is the main script for player behavior. In `Update()`, I check for spacebar presses with `Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.IsGameOver` (to prevent jumping after game over) and set the player’s vertical velocity with `rb.velocity = new Vector2(0, jumpForce)` to make the bird flap upward. I only trigger the `GameEvents.TriggerPlayerFlap()` event here, not directly calling the audio system, so the player logic doesn’t need to know anything about sound. For collision detection, I used `OnCollisionEnter2D(Collision2D other)` to check if the player hits an object tagged "Pipe", then call `GameManager.Instance.GameOver()` to end the game. Again, no UI or audio code here.
- **GameManager.cs**: I made this a singleton (more on that later) to manage global game state like score and game over status. The `AddScore()` method simply increments the score with `score++` and fires `GameEvents.TriggerScoreChanged(score)` to let the UI know the score updated(no direct text changes here). The `GameOver()` method sets `IsGameOver = true` and triggers `GameEvents.TriggerGameOver()` to signal failure, without touching the game over screen or hit sound effect directly.
- **PipeSpawner.cs**: This script handles pipe spawning. I used a coroutine `SpawnPipesCoroutine()` to spawn new pipe pairs every `spawnInterval = 1.5f` seconds, and added randomness to the pipe gaps with `randomY = Random.Range(-gapYOffset, gapYOffset)` to keep the gameplay unpredictable. It only creates pipes and adjusts their scale, no interaction with scores or UI.
- **PipeMovement.cs**: Simple leftward pipe movement here, `transform.Translate(Vector2.left * moveSpeed * Time.deltaTime)` in `Update()` moves pipes at 5 units per second, and I destroy pipes at `destroyX = -10f` to keep memory clean. No extra logic, just movement and cleanup.


#### 1.2 View Layer: Only Feedback (No Game Logic)
The View layer is all about showing/playing things in response to the Controller, no calculations, no collision checks, just reacting to events:
- **ScoreUI.cs**: This handles the on-screen score and game over panel. It subscribes to the `OnScoreChanged` event, so when the score updates, `UpdateScore(int newScore)` runs and sets `scoreDisplay.text = newScore.ToString()` to show the new number. For game over, it listens to `OnGameOver` and calls `ShowGameOver()` to activate the `gameOverPanel`. It never calculates scores or checks for failures, just shows what the Controller tells it to.
- **AudioManager.cs**: All sound effects live here. It subscribes to `OnPlayerFlap` (for flap sounds), `OnScoreChanged` (for score blips), and `OnGameOver` (for hit sounds). When those events fire, methods like `OnFlapSound()` run `audioSource.PlayOneShot(flapClip)` to play the right sound. I made sure it only plays sounds when told to.<br><br>

### 2. Events and Singletons
I used events and singletons to make sure the Controller and View layers don’t depend on each other directly. This way, I could change the UI or sound effects without breaking the core gameplay code (and vice versa).


#### 2.1 Event Pattern: No Direct Calls Between Layers
I created a static `GameEvents.cs` class to act as a "messenger" between layers. Instead of the Controller calling `AudioManager.PlayFlapSound()` or `ScoreUI.UpdateScore()`, it just sends an event, and any View scripts that care about that event respond:
- I defined three key events: `OnScoreChanged` (passes the new score as an int), `OnGameOver` (for game failure), and `OnPlayerFlap` (for flap sounds). These cover all the ways the Controller needs to talk to the View.
- In the Controller layer: `PlayerController.cs` calls `GameEvents.TriggerPlayerFlap()` when the player jumps; `GameManager.cs` triggers `OnScoreChanged` in `AddScore()` and `OnGameOver` in `GameOver()`. That’s it—no references to `AudioManager` or `ScoreUI` anywhere in the Controller code.
- In the View layer: `ScoreUI.cs` and `AudioManager.cs` subscribe to these events in their `Awake()` methods (e.g., `GameEvents.OnScoreChanged += UpdateScore`). They only do something when the event fires, and I unsubscribed all events in `OnDestroy()` to avoid memory leaks (I forgot to do this at first, and had weird sound glitches, *fixed it quick!*).


#### 2.2 Singleton Pattern: One Instance of Each Core System
I used singletons for `GameManager.cs`, `ScoreUI.cs`, and `AudioManager.cs` to make sure there’s only one copy of each system running, no duplicate score counters or multiple audio sources playing at once. Here’s how it works:
- For `GameManager.cs`, I added a static `Instance` variable (`public static GameManager Instance`) and checked in `Awake()` if an instance already exists—if yes, I destroy the new one, if no, I set `Instance = this`. This lets the Player or Pipe scripts call `GameManager.Instance.AddScore()` to update the score without hunting for the right GameManager object in the scene.
- I did the same for `ScoreUI.cs` and `AudioManager.cs`. This way, the Controller can trigger events knowing the View systems are there and unique, but still doesn’t need to reference them directly.<br><br>

## 3. How This Keeps Code Clean
The biggest win here is decoupling:
- The Controller layer (player/pipe/GameManager code) doesn’t know or care how the score is displayed or what sound plays when you jump. It just sends events and focuses on gameplay.
- The View layer (UI/audio) doesn’t know why the score changed or why the game ended, it just reacts to events and focuses on feedback.
- If I want to change the score font or swap the flap sound later, I only need to touch the View scripts, no messing with the core game logic.<br><br>


## Open-Source Assets
- [Industrial Pipe Platformer Tileset](https://wwolf-w.itch.io/industrial-pipe-platformer-tileset) - pipe sprites
- [Bird Sprites with Animations](https://carysaurus.itch.io/bird-sprites) - bird sprites
- [pixabay](https://pixabay.com/sound-effects/search/hit%20pipe/) - sound effects


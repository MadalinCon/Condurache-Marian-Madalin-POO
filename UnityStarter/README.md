# UnityStarter (2D)

Un set minimal de scripturi C# pentru a porni rapid un proiect 2D în Unity.

## Cum să folosești
1. Creează un proiect nou în Unity (2D).
2. Copiază folderul `Scripts` în `Assets/Scripts` în proiectul tău Unity.
3. Configurează scena:
   - Adaugă un `GameObject` numit `Player` cu `SpriteRenderer`, `Rigidbody2D` (Body Type: Dynamic, Freeze Z Rotation), `CapsuleCollider2D`/`BoxCollider2D`.
   - Creează un `Empty` drept copil al player-ului numit `GroundCheck` și poziționează-l sub picioare.
   - Adaugă scriptul `PlayerController2D` pe `Player` și setează câmpurile în Inspector (`groundCheck`, `groundLayers`).
   - Creează un `GameObject` numit `Main Camera` (implicit există) și adaugă `CameraFollow2D`. Setează `target` la `Player` și un `offset` (ex: z = -10).
   - Creează un `Empty` numit `GameManager`, adaugă scriptul `GameManager`.
   - Creează solul/platformele cu `Tilemap` sau `Sprite` + `Collider2D` și pune-le pe un `Layer` (ex: `Ground`). Setează `groundLayers` în `PlayerController2D` să includă acest layer.
4. Opțional: adaugă obiecte cu `Collectible` (trigger collider) și `EnemyPatrol2D` (cu două waypoint-uri).
5. Asigură-te că `Edit > Project Settings > Physics 2D` are o gravitație potrivită (implicit: y = -9.81) și că player-ul are `Rigidbody2D`.

## Input
- Mișcare: A/D sau săgeți (folosește `Horizontal`).
- Sărit: Space (folosește `Jump`).

## Scripturi incluse
- `PlayerController2D.cs`: mișcare stânga/dreapta, sărit, detecție sol.
- `CameraFollow2D.cs`: urmărire cameră cu smoothing.
- `GameManager.cs`: scor, restart scenă, pauză.
- `Collectible.cs`: item colectabil care adaugă scor.
- `EnemyPatrol2D.cs`: patrulare între două puncte.

## Note
- Scripturile folosesc sistemul clasic de input (`Input.GetAxisRaw` / `Input.GetButtonDown`). Dacă folosești noul Input System, adaptează metodele de input.
- Evită să schimbi `Time.timeScale` pentru pauză dacă folosești animații care necesită timp real; poți folosi `Animator.updateMode = UnscaledTime` unde e necesar.
Flappy Bird 2D — Cenário Dinâmico + Áudio Contextual (Caverna)

Jogo 2D estilo Flappy Bird com:

Cenário em loop alternando Céu → Caverna → Céu…

Mixagem de áudio por contexto via Snapshots do Audio Mixer:

Normal: música em loop + SFX limpos

Cave: música silenciada + SFX abafados (low-pass)

🎮 Como jogar

Pular: Space, clique do mouse ou toque (mobile).

Evite os obstáculos (canos) e marque pontos ao passar pelas aberturas.

⚙️ Como funciona o áudio (SFX/Música)

Dois AudioSources:

musicSource (trilha em loop)

sfxSource (efeitos: pulo, pontuação, colisão) com PlayOneShot

Detecção de ambiente:

Cada bloco do fundo (Céu ou Caverna) tem BoxCollider2D (Is Trigger) + BackgroundZone (isCaveZone)

O Player (tag Player) entra/sai das zonas; o BackgroundZone chama AudioManager.ZoneEnter/ZoneExit

Anti-bounce: o AudioManager mantém um contador de sobreposição e um debounce para evitar piscadas quando as bordas dos blocos encostam

Snapshots do Mixer:

Normal → Música em 0 dB; SFX com low-pass “aberto”

Cave → Música em −80 dB; SFX abafados (cutoff baixo)

Transição suave com TransitionTo() (ex.: 0.35s)

🧩 Estrutura dos scripts (pasta Assets/Scripts/)

AudioManager.cs — controla música/SFX e alterna snapshots Normal/Cave

BackgroundLooper.cs — move dois blocos de fundo, recicla e alterna sprites (céu/caverna)

BackgroundZone.cs — identifica se o bloco é caverna (isCaveZone) e notifica o AudioManager

FlyLogic.cs — lógica de pulo do player (chama AudioManager.PlayFlap())

canoLogic.cs — pontuação (chama AudioManager.PlayScore())

GameOver.cs — fim de jogo (chama AudioManager.PlayHit())

CanoSpawn.cs — spawner dos canos (sem áudio)

🖼️ Assets esperados

Sprites: bgSky, bgCave, chão, canos, pássaro

Áudio: music (loop), flapClip, scoreClip, hitClip

🛠️ Configuração rápida no Unity

Audio Mixer

Crie MainMixer

Grupos: Music e SFX

Adicione Lowpass no grupo SFX

Snapshots: Normal (Music 0 dB; SFX lowpass “aberto”) e Cave (Music −80 dB; SFX lowpass cutoff baixo ~900 Hz)

Cena

AudioManager com 2 AudioSources:

musicSource: loop ON, Output → Music

sfxSource: Output → SFX

Arraste os Snapshots Normal e Cave para o AudioManager

Player com Rigidbody2D, Collider2D, Tag = Player

Background: dois SpriteRenderer lado a lado (A e B) com BackgroundLooper apontando para ambos

Em cada bloco: BackgroundZone + BoxCollider2D (Is Trigger):

Céu → isCaveZone = false

Caverna → isCaveZone = true

▶️ Rodando o projeto

Abra o projeto no Unity (LTS recomendada)

Cena principal: Assets/Scenes/Main.unity

Clique Play

👥 Integrantes do time

Matheus Durigon Rodrigues

Joao Inacio Luis Zanin

Erick De Nardi

🔧 Troubleshooting

Sem som: verifique Audio Listener na Main Camera e se o ícone de áudio da Game view não está “mute”

SFX não abafa: confirme que o SFXSource → Output = SFX e que os Snapshots trocam ao entrar/sair da caverna

Pisca na transição: ajuste minSwitchDelaySeconds no AudioManager e a margem de colisão no BackgroundLooper (colliderHorizontalMargin)

📄 Licença

Uso educacional/acadêmico.

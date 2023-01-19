# UNDEAD SURVIVOR

![image-20230119153212524](https://user-images.githubusercontent.com/118050445/213386697-4c664a95-2179-4d55-bc08-21a887409a50.png)



- 게임 개발에 대한 이해도와 숙련도를 높이기 위해 캐쥬얼하고 최소한의 기능만으로 최고의 재미를 보장하는 장르인 뱀서라이크 계열의 게임을 만들어봤다.

- Game To Play - https://drive.google.com/file/d/1YgXHHbGiA_Kpw2S6dYT2AgyrommfWc5k/view?usp=sharing

- 구현 기능
  - 캐릭터 선택
  - UI
  - SpawningPool
  - 풀링 오브젝트
  - 아이템
  - 무기
  - 중간 보스
  - 보스
  - etc...
- 추가가 필요한 기능
  - 캐릭터 업그레이드
  - 패시브 아이템
  - 추가적인 무기
  - 몬스터 생성 패턴


## Scene 구성


![image](https://user-images.githubusercontent.com/118050445/213386652-b7ec359c-9ce5-4a62-b832-b1617b002970.png)

- Unity Splash Screen Show Scene
  - Unity Splash Screen을 보여주기 위한 Scene. Unity Splash Screen이 종료된 후 MainMenuScene으로 이동한다.

- MainMenu Scene
  - 초기 게임 시작 씬. 캐릭터를 선택할 수 있다.
- Game Scene
  - 게임을 플레이할 수 있는 씬.

## 구현 시스템

- 애니메이션

  각 오브젝트별로 추가적인 애니메이션을 추가했으며, UI에도 애니메이션이 첨부되었다.

<img src= "https://user-images.githubusercontent.com/118050445/213386727-87c1615a-3b8d-4b3f-b300-4d5d6ca11fb8.gif" width = "60px" height = "80px"> <img src= "https://user-images.githubusercontent.com/118050445/213386797-26b81c54-db72-4dd4-97a6-1f9cdf6f68ed.gif" width = "60px" height = "80px">
<img src= "https://user-images.githubusercontent.com/118050445/213386810-0ec5b0ec-2cec-4773-a522-86eb0365c52b.gif" width = "60px" height = "80px">
<img src= "https://user-images.githubusercontent.com/118050445/213386826-73aafc42-61e4-4ff8-90e4-93199913b55b.gif" width = "60px" height = "80px">
<img src= "https://user-images.githubusercontent.com/118050445/213386839-a33f57e8-d17c-479f-8506-4436a0f4c745.gif" width = "60px" height = "80px">
<img src= "https://user-images.githubusercontent.com/118050445/213386847-8e814c36-0649-4c89-902c-10df8f23a706.gif" width = "60px" height = "80px">
<img src= "https://user-images.githubusercontent.com/118050445/213386869-6df641d2-d16e-4d71-82af-f587b333cdaa.gif" width = "60px" height = "80px">

- UI 애니메이션
<p align="center">
<img src="https://user-images.githubusercontent.com/118050445/213387108-4b8f7ed4-e847-4b8a-9e3e-4cd73ac40304.gif" width = "50% height = "50%">
</p>

## Managers

  게임의 주요 기능들의 관리를 담당하는 매니저들이 존재한ㄷ다. 각 매니저들은 Managers 컴포넌트에 의해 생성 및 관리된다.

  - GameManager : 주요 게임 값의 관리(Player, Monster, Weapon 등)
  - UIManager : UI의 생성 및 관리
  - SoundManager : 게임 내 Sound의 실행 및 설정을 관리
  - DataManager : 게임 내의 고정적인 데이터를 저장 및 불러온다. CharacterData, MonsterData, WeaponData가 존재한다.
  - SceneManager : Scene의 이동 및 씬 동작 관리
  - PoolManager : 오브젝트 풀링 시스템을 관리
  - ResourceManager : 프로젝트 내 자원을 불러오고 제거하는 전반적인 동작을 관리
  - EventManager : 각 오브젝트들이 공용으로 사용되는 메소드를 저장하여 필요할 때 실행할 수 있도록 관리

## UI

### PopupUI와 SceneUI

- PopUI
  - 필요에 따라 생성하고, 제거할 수 있는 UI.
  - 나중에 생성된 UI는 최상단에 위치할 수 있도록 추가 팝업창이 생성될 때마다 sort Order값을 증가시키고, 닫히면 sort Order를 감소시킨다.

- SceneUI
  - 씬이 실행될 때 기본적으로 생성되는 UI.
  - 유저가 제거하거나 추가적으로 생성되지 않는다.
  - 필요에 따라서 한 씬에 여러개의 SceneUI가 생성될 수도 있으나, 이 게임의 경우 각 씬 당 1개의 SceneUI가 배치되어 있다.
   ![PlayerUI](https://user-images.githubusercontent.com/118050445/213389258-13a3b97b-6378-4320-b52d-716c6ae7acf9.PNG)
   
    번호 순서대로 소유 무기 출력 이미지 패널, 게임시간 출력 Text, 경험치 슬라이더, Level 출력 텍스트이다.
    
- WorldSpaceUI
  - ScreenSpace가 아닌 WorldSpace에 존재하는 UI. 캐릭터별 HP바을 달아주거나 DamageText를 WorldSpace상에 생성할 수 있다.

## Sound
각 오브젝트마다 컴포넌트로 Audio Source 컴포넌트를 추가하여 Sound를 실행하지 않고 SoundManager를 통해 필요할 때마다 AoudioSource를 생성하여 요구된 AudioClip를 삽입하여 실행한다.
```C#
    public void Play(string name, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(name, type);
        Play(audioClip, type, pitch);
    }

	public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
	{
        if (audioClip == null)
            return;

		if (type == Define.Sound.Bgm)
		{
			AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
			if (audioSource.isPlaying)
				audioSource.Stop();

            BGM = (Define.BGMs)System.Enum.Parse(typeof(Define.BGMs), audioClip.name);

            audioSource.pitch = pitch;
			audioSource.clip = audioClip;
			audioSource.Play();
		}
		else
		{
			AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
			audioSource.pitch = pitch;
			audioSource.PlayOneShot(audioClip);
		}
	}
  
  	AudioClip GetOrAddAudioClip(string name, Define.Sound type = Define.Sound.Effect)
    {
		AudioClip audioClip = null;

		if (type == Define.Sound.Bgm)
		{
            string path = $"Audio/BGM/{name}";
			audioClip = Managers.Resource.Load<AudioClip>(path);
		}
		else
		{
			if (_audioClips.TryGetValue(name, out audioClip) == false)
			{
                string path = $"Audio/Effect/{name}";
                audioClip = Managers.Resource.Load<AudioClip>(path);
				_audioClips.Add(name, audioClip);
			}
		}

		if (audioClip == null)
			Debug.Log($"AudioClip Missing ! {name}");

		return audioClip;
    }
```
- GameMenuUI에서 게임상의 Sound의 Volumn과 BGM을 설정할 수 있다.
<p align="center">
<img src= "https://user-images.githubusercontent.com/118050445/213403325-02bd0403-856f-4628-b57f-913d1c0c35f7.PNG" width = "50% height = "50%">
</p>
## Weapon

각 캐릭터는 전용 무기를 가지고 시작하며, 게임 진행에 따라 추가적인 무기 획득 및 무기의 능력치를 증가시킬 수 있다.
- 무기의 능력치는 무기의 레벨과 플레이어의 능력치에 따라 결정되며, DataManager를 통해 각 무기의 레벨별 능력치를 불러와 무기ID와 현재 무기 레벨에 맞는 데이터를 무기 능력치에 저장한다.
```C#
    protected virtual void SetWeaponStat()
    {
        if (_level > 5)
            _level = 5;

        _damage = (int)(_weaponStat[_level].damage * ((float)(100+ _playerStat.Damage)/100f));
        _movSpeed = _weaponStat[_level].movSpeed;
        _force = _weaponStat[_level].force;
        _cooldown = _weaponStat[_level].cooldown * (100f/(100f +_playerStat.Cooldown));
        _size = _weaponStat[_level].size;
        _penetrate = _weaponStat[_level].penetrate;
        _countPerCreate = _weaponStat[_level].countPerCreate + _playerStat.Amount;
    }
```

- 현재 구현 무기 - Knife, Fireball, SpinWeapon, Poison, Shotgun, Lightning

<img src= "https://user-images.githubusercontent.com/118050445/213397871-6552ff87-e7dc-404d-912f-6846f2228c72.gif" alt="text" width="30%" height="30%"> <img src= "https://user-images.githubusercontent.com/118050445/213397892-9a159b47-d3f4-470e-b141-aa4b3b1fb5cc.gif" alt="text" width="30%" height="30%"> <img src= "https://user-images.githubusercontent.com/118050445/213397899-14df5e24-3a2a-406d-893d-1ab6c7b0fa4e.gif" alt="text" width="30%" height="30%">


## PlayerStat
 캐릭터의 주요 능력치는 MaxHp, MoveSpeed, Damage, Defense, Cooldown, Amount로 구성된다.
 - MaxHp : 최대 Hp. 1당 1만큼 증가한다.
 - MoveSpeed : 이동 속도. 1당 1만큼 증가한다.
 - Damage : 공격력. 10당 10%만큼 공격력이 증가한다.(합계산)
 - Defense : 방어력. 1당 1만큼 증가한다.
 - Cooldown : 무기 재사용 대기시간. 10당 10%만큼 대기시간이 감소한다.(곱계산)
 - Amount : 무기 생성 개수. 한번에 생성하는 무기의 개수가 증가한다. 1당 1만큼 증가한다.
 
<p align="center">
<img src= "https://user-images.githubusercontent.com/118050445/213406700-4d3f1217-0df9-479e-adb2-b1157b4b60af.PNG" alt="text" width="50%" height="50%">
</p>

## Item
  몬스터는 사망 시 Exp과 일정 확률로 Item을 생성한다. 캐릭터는 ItemGetterObject를 통해 특정 거리의 아이템의 존재를 확인할 수 있으며, 아이템을 확인할 시 아이템이 캐릭터를 향헤 이동하며 캐릭터와 충돌 시 각 아이템의 OnItemEvent를 실행한다.
  
<p align="center">
<img src= "https://user-images.githubusercontent.com/118050445/213404095-ff6fb57f-997d-464e-a811-c83c217ba9f8.gif" alt="text" width="50%" height="50%">
</p>

- Exp : 특정 값만큼 캐릭터의 Exp를 증가시킨다.

<p align="center">
<img src= "https://user-images.githubusercontent.com/118050445/213404010-26593362-7aad-4fff-81df-99e42e3331c4.gif" alt="text" width="50%" height="50%">
</p>

- Health : 특정 값만큼 캐릭터의 체력을 회복시킨다.

<p align="center">
<img src= "https://user-images.githubusercontent.com/118050445/213404146-cb846101-234b-4776-8985-123da72e05be.gif" alt="text" width="50%" height="50%">
</p>

- Magnet : 월드맵에 존재하는 모든 Item을 캐릭터를 향해 이동시킨다.

<p align="center">
<img src= "https://user-images.githubusercontent.com/118050445/213404176-cec7ab90-7e15-4b41-ae4d-256efc8a07c3.gif" alt="text" width="50%" height="50%">
</p>

- ItemBox : 캐릭터를 향해 움직이지 않는 Item이다. 획득 시 ItemBoxOpenUI를 생성하며, 이를 통해 랜덤으로 무기를 획득 및 강화할 수 있다. 만약 더이상 무기를 강화하거나 획득할 수 없다면 Health 아이템을 획득한다.

<p align="center">
<img src= "https://user-images.githubusercontent.com/118050445/213404163-b701615f-9192-474a-af70-add9af5ed383.gif" alt="text" width="50%" height="50%">
</p>


## SpawningPool
- 캐릭터의 일정 범위에는 몬스터를 생성하는 SpawningPool이 존재하며, 랜덤의로 위치를 지정하여 몬스터를 생성한다.
- 게임 시간이 1분이 지날 때마다 중간보스가 생성되며, 중간보스는 일반 몬스터보다 더 강한 능력치를 가진다.
- 5분이 지날 시 보스가 생성되며, 보스가 사망 시 게임을 승리한다.

### 몬스터 종류
- 근접 : 몬스터들은 기본적으로 플레이어와 접촉 시 데미지를 준다. 
- 원거리 : 특정 몬스터는 플레이어의 위치를 향해 bullet을 발사한다.
- 중간보스 : 일반 몬스터보다 크기가 더 크고 무게가 더 나가며, 일반몬스터에서 중간보스로 인한 보정만큼 능력치가 향상된다.
- 보스 : 다른 몬스터들과 독립적으로 존재하며, 1개 이상의 스킬을 가지고 있고 스킬들 중 랜덤으로 1개를 선택하여 사용한다. 스킬은 재사용 대기시간이 존재한다.

### Boss Skill Example

  - Blink - 시간정지 후 플레이어 주변으로 이동
<p align="center">
<img src= "https://user-images.githubusercontent.com/118050445/213416839-579a61dd-ebb0-445f-ac83-7ffdad75fa83.gif" width ="50%" height="50%">




  - Rush - 플레이어 위치로 일직선으로 돌진. 3번 반복
<p align="center">
<img src= "https://user-images.githubusercontent.com/118050445/213416857-4163c548-b036-489b-9c92-dd354db46dfc.gif" width ="50%" height="50%">
</p>

## 오브젝트 풀링
몬스터 및 투사체가 필요할 때마다 생성하고 파괴하면 메모리를 할당하고 해제하는 일이 반복된다. 이로인한 가비지 컬렉터가 지속적으로 발생하면 CPU에 큰 부담이 발생하기 때문에 풀링 매니저를 통해 오브젝트 풀링을 관리했다.

### 관리 방법
  1. 오브젝트 생성 요구
  2. 풀링 오브젝트 확인
  3. 풀링 오브젝트라면, 풀에 오브젝트를 생성 및 배치
  4. 풀에서 오브젝트를 빼오고 활성화
  5. 만약 추가적인 생성 요구가 존재하고, 풀에 오브젝트가 존재하지 않다면 풀에 추가적인 오브젝트를 생성
  6. 오브젝트 파괴를 요구하면 오브젝트를 비활성화하고 다시 풀에 배치
  
<p align="center">
  <img src ="https://user-images.githubusercontent.com/118050445/213412019-3c9bf675-5b8e-4c9e-af10-e2f95c4ba928.png">
</p>

## RePosition 시스템
  플레이어가 이동할 때 카메라 밖으로 몬스터가 빠져나갈 경우 플레이어의 주변 위치로 이동시킨다.
  
<p align="center">
<img src ="https://user-images.githubusercontent.com/118050445/213412923-035b56d0-b17c-4578-815c-0057c7df07e0.gif">
</p>

## Tile RePosition
플레이어의 위치한 타일에서 벗어날 시 타일을 플레이어의 현재 타일 위치에 맞춰 재배치함으로써 한정적인 타일을 이용하여 마치 무한한 범위의 맵인 듯한 느낌을 제공한다.
<p align="center">
<img src="https://user-images.githubusercontent.com/118050445/213415378-5e56e2b2-bb21-4889-8f14-212a23c8caa5.gif" width="50%" height="50%">
</p>


# 개발 결과
<p align="center">
<img src="https://user-images.githubusercontent.com/118050445/213421054-cb5d8223-3279-4d0d-9623-08725e602b18.gif" width="70%" height="70%">
</p>

## ==========================
- 제작기간 : 20일
- 잠고 에셋
  - 언데드 서바이버 에셋 팩 : https://www.youtube.com/watch?v=MmW166cHj54&t=2s
  - 2d Flat Explosion : https://assetstore.unity.com/packages/2d/textures-materials/2d-flat-explosion-66932
  - 8-Bit Sfx : https://assetstore.unity.com/packages/audio/sound-fx/8-bit-sfx-32831
  - Casual Game BGM #5 : https://assetstore.unity.com/packages/audio/music/casual-game-bgm-5-135943
  - 2D Potions Pixel Art : https://assetstore.unity.com/packages/2d/gui/icons/2d-potions-pixel-art-196023
  - Buttons Set : https://assetstore.unity.com/packages/2d/gui/buttons-set-211824
  - Monsters_Creatures_Fantasy : https://assetstore.unity.com/packages/2d/characters/monsters-creatures-fantasy-167949

# UNDEAD SURVIVOR

![image-20230119153212524](https://user-images.githubusercontent.com/118050445/213386697-4c664a95-2179-4d55-bc08-21a887409a50.png)



- 게임 개발에 대한 이해도와 숙련도를 높이기 위해 캐쥬얼하고 최소한의 기능만으로 최고의 재미를 보장하는 장르인 뱀서라이크 계열의 게임을 만들어봤다.

- Game To Play - https://drive.google.com/file/d/1DCUpQkDisK1UOMmwQSigFkcPXnLAeTBA/view?usp=sharing
- 프로젝트 설명서 - https://github.com/newtron-vania/Vampire-Survivor/raw/main/%EC%96%B8%EB%8D%B0%EB%93%9C%20%EC%84%9C%EB%B0%94%EC%9D%B4%EB%B2%84%20%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8%20%EC%84%A4%EB%AA%85%EC%84%9C.hwp
- 플레이 영상 - https://youtu.be/Xxo9mynXYRA

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

## Weapon

각 캐릭터는 전용 무기를 가지고 시작하며, 게임 진행에 따라 추가적인 무기 획득 및 무기의 능력치를 증가시킬 수 있다.
- 무기의 능력치는 무기의 레벨과 플레이어의 능력치에 따라 결정되며, DataManager를 통해 각 무기의 레벨별 능력치를 불러와 무기ID와 현재 무기 레벨에 맞는 데이터를 무기 능력치에 저장한다.

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
  몬스터는 사망 시 Exp과 일정 확률로 Item을 생성한다. 캐릭터는 ItemGetterObject를 통해 특정 거리의 아이템의 존재를 확인할 수 있으며, 아이템을 확인할 시 아이템이 캐릭터를 향해 이동하며 캐릭터와 충돌 시 각 아이템의 OnItemEvent를 실행한다.
  
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
- 캐릭터의 일정 범위에는 몬스터를 생성하는 SpawningPool이 존재하며, 랜덤으로 위치를 지정하여 몬스터를 생성한다.
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


# 개발 결과
<p align="center">
<img src="https://user-images.githubusercontent.com/118050445/213421054-cb5d8223-3279-4d0d-9623-08725e602b18.gif" width="70%" height="70%">
</p>

##
- 제작기간 : 20일
- 참고 에셋
  - 언데드 서바이버 에셋 팩 : https://assetstore.unity.com/packages/2d/undead-survivor-assets-pack-238068
  - 2d Flat Explosion : https://assetstore.unity.com/packages/2d/textures-materials/2d-flat-explosion-66932
  - 8-Bit Sfx : https://assetstore.unity.com/packages/audio/sound-fx/8-bit-sfx-32831
  - Casual Game BGM #5 : https://assetstore.unity.com/packages/audio/music/casual-game-bgm-5-135943
  - 2D Potions Pixel Art : https://assetstore.unity.com/packages/2d/gui/icons/2d-potions-pixel-art-196023
  - Buttons Set : https://assetstore.unity.com/packages/2d/gui/buttons-set-211824
  - Monsters_Creatures_Fantasy : https://assetstore.unity.com/packages/2d/characters/monsters-creatures-fantasy-167949

# Tetris
Tetris Game with Uinty 3D

# How to Play
## Control
Left Arrow: 좌로 이동
Right Arrow: 우로 이동
Up Arrow: 90도 회전
Down Arrow: 떨어지는 속도 빠르게
Space Key: 테트리스 블록을 떨어뜨림

## Falling Cycle
테트로미노가 떨어지는 주기는 1초 간격으로 스테이지 레벨이 높아질 때마다 짧아짐
>Falling Cycle -= 0.2f * Level; 

## Combo
한 행을 지웠을 때 콤보가 발생하고, 다음 테트로미노 블록으로 
새로운 행을 지웠을 때 콤보가 상승하고, 실패했을 경우 0으로 초기화된다.

## Score
한 행을 지울 때마다 Combo수에 비례하여 100의 점수를 획득한다.
>Score += 100 * Combo;

한 스테이지 레벨마다 1500 * level만큼의 Score를 획득해야 다음 레벨로
넘어갈 수 있다.

# Development
## Tetromino
4개의 Block을 하나의 Transform으로 묶어 관리한다.

## Control
Tetromino를 컨트롤하기 위해서는 맵 범위를 벗어나는지, 
다른 블록과 겹치지는 않는지 확인할 필요가 있었다. 그래서 맵을 생성하고 크기를 저장할 컴포넌트인 Grid와
더 이상 움직일 수 없게 된 Tetromino를 블록 단위로 분리하여 저장할 Block Map 컴포넌트를 만들었다.

1.Move
Tetromino를 움직일 때마다 Grid를 벗어나지 않는지 확인하고, 자식 Block들의 위치 값을 Block Map에서 찾아
해당 좌표에 이미 블록이 있는 경우 움직일 수 없다고 판단한다.

2.Rotate
임의로 정해진 Pivot을 중심으로 Tetromino를 회전시킨다.

## Block Map
기존 2차원 배열로 map을 만들었는데, 각 요소에 접근하기가 어려워 Generic List로 변경하여 
보다 각 요소에 접근하기가 용이해졌다. 다만, List는 생성 시 null 값으로 초기화되지 않아 직접 null 값을 넣어줘야하는 번거로움이 있었다. 
행이 전부 블록으로 가득 차 있는지 확인하기가 매우 번거로웠었는데 간단하게 바꿀 수 있었다.

### 변경 전
>int count = 0;  
for(int col = 0; col < map.GetLengt(0); ++col) {  
  if(map[row][col] != null) ++count;  
}  
if(count == grid.Width) deleteRow(row);  

### 변경 후
>map[row].TrueForAll((block) => {  
  return block != null;  
});    

## 수정할 것
1. Tetromino를 회전할 때 pivot의 위치 수정
- 자식 Block들의 좌표값을 그 수로 나누어 pivot값을 구했으나, 해당 pivot으로 회전할 시
자식 Block들의 좌표값이 소수점 단위로 떨어지게 됨. 맵의 좌표는 정수형 좌표이므로 이를 보정할 수 있는 방법이 필요할듯.
2. 회전할 수 없는 상태일 때, 이동하여 회전하기

# Play Video
## version 1.0
[![Video Label](https://img.youtube.com/vi/JfV1ewq8hU8/0.jpg)](https://youtu.be/JfV1ewq8hU8)

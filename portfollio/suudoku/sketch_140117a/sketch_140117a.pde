int[] Numbers={
    1,0,0,0,0,7,0,9,0,
    0,3,0,0,2,0,0,0,8,
    0,0,9,6,0,0,5,0,0,
    0,0,5,3,0,0,9,0,0,
    0,1,0,0,8,0,0,0,2,
    6,0,0,0,0,4,0,0,0,
    3,0,0,0,0,0,0,1,0,
    0,4,0,0,0,0,0,0,7,
    0,0,7,0,0,0,3,0,0}
    ,oriNumbers={
    1,0,0,0,0,7,0,9,0,
    0,3,0,0,2,0,0,0,8,
    0,0,9,6,0,0,5,0,0,
    0,0,5,3,0,0,9,0,0,
    0,1,0,0,8,0,0,0,2,
    6,0,0,0,0,4,0,0,0,
    3,0,0,0,0,0,0,1,0,
    0,4,0,0,0,0,0,0,7,
    0,0,7,0,0,0,3,0,0 };
PFont font;
void setup(){
  size(450,450);
  font=loadFont("AgencyFB-Reg-48.vlw");
  textAlign(CENTER);
  rectMode(CENTER);
  textSize(width/9);
  stroke(0);
  screen();
}
void draw(){}
void keyPressed(){
     check(0);
}
void mousePressed(){
  inputNo();
  screen();
}
void inputNo(){
  int pos = mouseX/(width/9)+mouseY/(height/9)*9;
  if(mouseButton==LEFT){
    Numbers[pos]++;
      if(Numbers[pos]>9){
        Numbers[pos]=0;
      }
  }
    if( mouseButton == RIGHT){
      Numbers[pos]=0;
  }
    oriNumbers[pos]=Numbers[pos];
}
void screen(){
  background(255);
  Number();
  lines();
}
void Number(){
  for(int i=0;i<81;i++){
    if(Numbers[i]>0){
      fill(127,127,127);
      if(oriNumbers[i]>0) fill(0);
      text(Numbers[i],(width/9)*(i%9+0.5),(height/9)*(i/9+1)-3);
    }
  }
}
void lines(){
  for(int i = 0 ; i < 10 ; i++){
    if(i%3==0)strokeWeight(3);
    line(width/9*i,0,width/9*i,height);
    line(0,height/9*i,width,height/9*i);
    strokeWeight(1);
  }
}
boolean InputCheck(int pos,int x){
  int row=pos/9;
  int col=pos%9;
  int LU=9*(row/3)*3+(col/3)*3;
  for(int i=0;i<9;i++){
    if(Numbers[row*9+i]==x) return false;//yoko
    if(Numbers[col+i*9]==x) return false;//tate
  }
  for(int i=0;i<3;i++){
    for(int t=0;t<3;t++){
      if(Numbers[LU+i*9+t]==x) return false;//masu
    }
  }
  return true;
}
void check(int pos){
  int i, x, newPos;
  if (pos==81) {
    screen();
    return;
  }
  for (newPos=pos; newPos<81; newPos++) {
    if (Numbers[newPos]==0) break;
  }
  for (x=1; x<=9; x++) {
    if (InputCheck(newPos,x)) {
      Numbers[newPos]=x;
      check(newPos+1);
      Numbers[newPos]=0;
    }
  }
}


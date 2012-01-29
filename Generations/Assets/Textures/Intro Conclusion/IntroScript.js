var intro : Texture2D;
var intro2 : Texture2D;
var introEnter : Texture2D;
var introBlack : Texture2D;
var delay : float;

private var splash : boolean = false;
private var introOver : boolean = false;

function Start () {

guiTexture.texture = introBlack;

yield WaitForSeconds(1);

guiTexture.texture = intro;

yield WaitForSeconds(delay);

guiTexture.texture = intro2;

yield WaitForSeconds(4);

introOver = true;
splash = true;

}

function Update(){

if(introOver){

	if(splash){
		Reset();
		}

	else
		Reset2();
	}
	
	if(Input.GetButtonUp("Jump")){
       Application.LoadLevel("TestScene");
    }


}

function Reset(){

guiTexture.texture = introEnter;
yield WaitForSeconds(1);
splash = false;

}

function Reset2(){

guiTexture.texture = introBlack;
yield WaitForSeconds(0.3);
splash = true;

}
"use strict";

import * as modView from "./view.js";

//constants
const intLives = 4; //default starting lives

//background
export const imgCloud1 = document.getElementById("imgCloud1");
export const imgCloud2 = document.getElementById("imgCloud2");
export const imgCloud3 = document.getElementById("imgCloud3");
export const imgCloud4 = document.getElementById("imgCloud4");
export const imgCloud5 = document.getElementById("imgCloud5");
export const imgCloud6 = document.getElementById("imgCloud6");
export const imgCloud7 = document.getElementById("imgCloud7");
export const imgFish1 = document.getElementById("imgFish1");
export const imgFish2 = document.getElementById("imgFish2");
export const imgFish3 = document.getElementById("imgFish3");
export const imgFish4 = document.getElementById("imgFish4");

//play elements
export const imgSub = document.getElementById("imgSub");
export const imgBoat = document.getElementById("imgBoat");
export const imgDepth = document.getElementById("imgDepth");
export const btnStart = document.getElementById("btnStart");
export const scrGame = document.getElementById("scrGame");
//win/lose etc
export const divYoumissed = document.getElementById("divYoumissed");
export const divHit = document.getElementById("divHit");
export const divPlayagain = document.getElementById("divPlayAgain");
export const divOver = document.getElementById("divOver");
export const divIntroText = document.getElementById("divIntroText");
export const divIntroTextHeader = document.getElementById("divIntroTextHeader");
//level
export const constLevel1Timer = 260000;
export const constLevel2Tiner = 120000;
export const constLevel3Timer = 140000;

//sub speed per level (timer interval)
export const constSubLevel1Speed = 4000;
export const constSubLevel2Speed = 3000;
export const constSubLevel3Speed = 2000;

//school of fish start positions
export const constFish1X = -560;
export const constFish2X = -590;
export const constFish3X = -540;
export const constFish4X = -550;
//cloud start positions
export const constCloud1X = 100;
export const constCloud2X = 230;
export const constCloud3X = 730;
export const constCloud4X = 530;
export const constCloud5X = 830;
export const constCloud6X = 1030;
export const constCloud7X = 1230;
//cloud wrap start positions
export const constCloud1WrapX = -190;
export const constCloud2WrapX = -181;
export const constCloud3WrapX = -181;
export const constCloud4WrapX = -150;
export const constCloud5WrapX = -190;
export const constCloud6WrapX = -220;
export const constCloud7WrapX = -390;
//sub start position
export const constSubTop = 700;
export const constSubLeft = 5;
//player
export const objPlayer = {
  intScore: 0,
  intLevel: 1,
  intTimeLeft: constLevel1Timer,
  intLivesLeft: intLives,
  blnDroppedDepth: false,
  blnStopPlay: false,
  blnPlayAgain: false,
};
//sub for handling movement inside timer
export const objSub = {
  intRightHandMargin: scrGame.clientWidth - 160,
  intDefaultSubX: constSubLeft,
  intSubX: constSubLeft,
  blnSubShrink: false,
  intSubShrinkAmt: 0,
  imgSubMove: imgSub,
  intSubSpeed: 0,
};

export const funcConvertToNumber = (value) => {
  //converts a string containing a number to a pure number
  //e.g. a123g45 returns 12345
  let strTemp = value;
  strTemp = Number(strTemp.replace(/[^0-9]/g, ""));
  return strTemp;
};
//rog's patented delay routine :)
export const funcSleep = (milliseconds) => {
  let curDate = new Date();
  //get current time
  let startTime = curDate.getTime();
  //set both times to match
  let curTime = startTime;

  while (curTime - startTime <= milliseconds) {
    curDate = new Date();
    curTime = curDate.getTime();
  }
};
export const funcRestartLevel = (blnCompleteRestart = false) => {
  //reset level timer
  switch (objPlayer.intLevel) {
    case 1:
      objPlayer.intTimeLeft = constLevel1Timer;
      break;
    case 1:
      objPlayer.intTimeLeft = constLevel2Tiner;
      break;
    case 3:
      objPlayer.intTimeLeft = constLevel3Timer;
      break;
  }

  //if complete reset, set lives back to default adn reset score
  if (blnCompleteRestart) {
    objPlayer.intLivesLeft = intLives;
    objPlayer.intScore = 0;
    //restart screen updater
    modView.funcUpDateScreen();
  }

  divPlayagain.hidden = true;
  //allow key input
  objPlayer.blnPlayAgain = false;
  objPlayer.blnStopPlay = false;
};

export const funcNextLevel = () => {
  //reset level timer
  switch (objPlayer.intLevel) {
    case 1:
      objPlayer.intTimeLeft = constLevel1Timer;
      break;
    case 1:
      objPlayer.intTimeLeft = constLevel2Tiner;
      break;
    case 3:
      objPlayer.intTimeLeft = constLevel3Timer;
      break;
  }

  //if complete reset, set lives back to default
  objPlayer.intLivesLeft = intLives;
  divPlayagain.hidden = true;
  //restart screen updater
  modView.funcUpDateScreen();
  //allow key input
  objPlayer.blnPlayAgain = false;
  objPlayer.blnStopPlay = false;
};

const funcSetKeypressHandler = function () {
  //handles player input

  window.addEventListener("keypress", (key) => {
    //if play halted accept no keyboard inputs
    if (objPlayer.blnStopPlay) {
      return;
    }

    if (objPlayer.blnPlayAgain) {
      //see what key is pressed
      switch (key.code) {
        case "KeyY":
          funcRestartLevel(true);
          break;
        case "KeyN":
          window.close();
          break;
      }
    } else {
      //see what key is pressed
      switch (key.code) {
        case "Space":
          //drop depth charge
          modView.funcMoveShip(2);
          break;
        case "KeyD":
          //move right
          modView.funcMoveShip(1);
          break;
        case "KeyA":
          //move left
          modView.funcMoveShip(0);
          break;
      }
    }
  });
};

const funcSetTimers = () => {
  //sets the various timers for game elements
  modView.funcTimerClouds();
  modView.funcSetupTimerSub();
  modView.funcUpDateScreen(true);
  modView.funcTimerMoveFish();
};

const funcCreateButtonEvent = () => {
  //creates button click handler this starts the game
  btnStart.addEventListener("click", () => {
    //get background music object src
    const audPlay = new Audio(
      document.getElementById("sndBackground").getAttribute("src")
    );

    audPlay.setAttribute("loop", "true");
    //play background music asynchronously
    audPlay.play().async;
    //hide start button
    btnStart.style.display = "none";
    //hide intro text
    divIntroText.hidden = true;
    divIntroTextHeader.hidden = true;
    //show game screen
    scrGame.style.display = "block";
    funcSetTimers();
    funcSetKeypressHandler();
    //show boat
    imgBoat.hidden = false;
    //set sub top - do this so has properties as collision detection needs them
    imgSub.style.top = "700px";
    //est sub width
    imgSub.style.width = "160px";
    //show sub
    imgSub.hidden = false;
  });
};

export const funcInit = () => {
  // creates button event handler as that clicked starts the game

  //hides the game area
  scrGame.style.display = "none";
  //init player and level
  objPlayer.intLivesLeft = intLives;
  objPlayer.intLevel = 1;
  objPlayer.intTimeLeft = constLevel1Timer;
  objPlayer.intScore = 0;
  objSub.intSubSpeed = constSubLevel1Speed;
  //create start button handler
  funcCreateButtonEvent();
};

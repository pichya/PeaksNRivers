# Peaks And Rivers

![TitleScreen](../../blob/master/src/Assets/Images/tItleScreen.png)

## Summary
Peaks and Rivers is our submission to the Colorado Lottery Game Jam 2020. This project is a prototype for a Monitor style lottery game where players can fill out a form before each game starts and wager on how many turns it will take to reach the end, how many ladders and rivers will be taken, the grid square numbers on the 10th and 20th turns...etc. The visual theme and design is inspired by the scenic geography of the Colorado mountains and rivers.

## Inspiration
**Scenic Colorado**

![ColoradoRockies](../../blob/master/src/Assets/Images/RockyMountains.jpg)
Towering, snow-capped mountains, waterfall hikes, natural hot springs tucked into valleys, golden galaxies of aspen trees in the fall—Colorado is a scenic geographic wonder. The views of the Rocky Mountains come together in a serene blend of alpine lakes, meadows, and forests. Colorado is home to some breathtaking landscapes that’s enjoyable all year, including several national and state parks; more than 50 hikeable fourteeners.

**Snakes And Ladders**

![SnakesAndLadders](../../blob/master/src/Assets/Images/childsgame.jpg)
Snakes and Ladders is an ancient board game regarded today as a worldwide classic. It originated in India as part of a family of dice board games such as parchisi. It was known as Moksha Patam and is played between two or more players on a game board with numbered, gridded squares. A number of ladders and snakes are pictured on the board, each connecting two specific board squares. The object of the game is to navigate one’s game piece, according to die rolls, from the start to the finish, helped or hindered by ladders and snakes respectively. The Snakes represents Vice and Ladders represents Virtue where karma is essential part of the game's outcome.

## Sample Peaks and Rivers Monitor Game Playslip
![Sample Playslip](../../blob/master/src/Assets/Images/playslip.jpg)

## Technology / Game Assets Used
Peaks and Rivers was developed in Unity 3D, version 2019.3.14. We decided to use an isometric camera view for the game screen to better enhance the gameboard design and the player's ascent to the top. Introducing the Low Poly Assets into the environment graphics is less distracting than high-resolution graphics and keeps the focus on the gameplay. The Unity Asset Store was the source for all the (Free!) Game Assets used inside the game. 

![UnityIDE](../../blob/master/src/Assets/Images/IDE.png)

**Forest - Low Poly Toon Battle Arena / Tower Defense Pack**
https://assetstore.unity.com/packages/3d/environments/forest-low-poly-toon-battle-arena-tower-defense-pack-100080

**LowPoly Trees and Rocks**
https://assetstore.unity.com/packages/3d/vegetation/lowpoly-trees-and-rocks-88376

**Dice Pack Light**
https://assetstore.unity.com/packages/templates/packs/dice-pack-light-165

**Original Audio Design / Music Loops created by**
http://seriallab.com/

**Title Screen / Logo Design by**
https://minimonstermedia.com/

## Incorporating web3/blockchain technology

**Unity and Nethereum Library**
There were a couple of options of bring blockchain into the Unity production workflow and game logic, including some Unity Store assets. Ultimately we settled on importing the Nethereum library into Unity. The Nethereum webpage had a Unity Game Dev section with github links to sample Unity projects. The Nethereum Library can be considered a C# version of the Web3 library that is offered in JavaScript. We were able to import the Library and Plugins into our game framework
http://docs.nethereum.com/en/latest/unity3d-introduction/
https://github.com/Nethereum/Unity3dSimpleSample,
![NethereumLibrary](../../blob/master/src/Assets/Images/Netthereum.png)

**Acquiring Ether & Smart Contract C# Wrappers**
After installing Nethereum into the Peaks and Rivers project, the next step was to install and run the Metamask Chrome extension to create an ethereum account and acquire some test network ether. With an ether account balance we were ready to connect the game to a smart contract. To connect our Unity with smart contracts written in the language Solidity, we would need to have a custom C# wrapper. We did locate a sample Unity C# wrapper written for a high score contract.
https://www.raywenderlich.com/5509-unity-and-ethereum-why-and-how#toc-anchor-005

![Metamask](../../blob/master/src/Assets/Images/Ropsten.png)

**Random Dice Roll and the VRF Smart Contract**
We were able to use the Ropsen ether credits to test smart contracts statically inside the browser using the Ethereum Remix IDE. Additionally, we located a dice roll smart contract that leverages the Chainlink verifiable random function (VRF). A transaction is needed to request a random number and another transaction is need when the random number is received. Next steps, would be to create a Unity C# Wrapper for the following Solidity code for the random dice roll smart contract so we could use it within Peaks and Rivers game and future C# Unity projects.
https://medium.com/coinmonks/how-to-generate-random-numbers-on-ethereum-using-vrf-8250839dd9e2

```
pragma solidity 0.6.2;

import "https://raw.githubusercontent.com/smartcontractkit/chainlink/develop/evm-contracts/src/v0.6/VRFConsumerBase.sol";

contract VRFTestnetD20 is VRFConsumerBase {
    using SafeMath_Chainlink for uint;

    uint256[] public d20Results;
    
    bytes32 internal keyHash;
    uint256 internal fee;
    
    /**
     * @notice Constructor inherits VRFConsumerBase
     * @dev Ropsten deployment params:
     * @dev   _vrfCoordinator: 0xf720CF1B963e0e7bE9F58fd471EFa67e7bF00cfb
     * @dev   _link:           0x20fE562d797A42Dcb3399062AE9546cd06f63280
     */
    constructor(address _vrfCoordinator, address _link)
        VRFConsumerBase(_vrfCoordinator, _link) public
    {
        vrfCoordinator = _vrfCoordinator;
        LINK = LinkTokenInterface(_link);
        keyHash = 0xced103054e349b8dfb51352f0f8fa9b5d20dde3d06f9f43cb2b85bc64b238205;
        fee = 10 ** 18;
    }
    
    /** 
     * @notice Requests randomness from a user-provided seed
     * @dev This is only an example implementation and not necessarily suitable for mainnet.
     * @dev You must review your implementation details with extreme care.
     */
    function rollDice(uint256 userProvidedSeed) public returns (bytes32 requestId) {
        require(LINK.balanceOf(address(this)) > fee, "Not enough LINK - fill contract with faucet");
        bytes32 _requestId = requestRandomness(keyHash, fee, userProvidedSeed);
        return _requestId;
    }
    
    /** 
     * @notice Modifier to only allow updates by the VRFCoordinator contract
     */
    modifier onlyVRFCoordinator {
        require(msg.sender == vrfCoordinator, 'Fulfillment only allowed by VRFCoordinator');
        _;
    }

    /**
     * @notice Callback function used by VRF Coordinator
     * @dev Important! Add a modifier to only allow this function to be called by the VRFCoordinator
     * @dev This is where you do something with randomness!
     * @dev The VRF Coordinator will only send this function verified responses.
     * @dev The VRF Coordinator will not pass randomness that could not be verified.
     */
    function fulfillRandomness(bytes32 requestId, uint256 randomness) external override onlyVRFCoordinator {
        uint256 d20Result = randomness.mod(20).add(1);
        d20Results.push(d20Result);
    }
    
    /**
     * @notice Convenience function to show the latest roll
     */
    function latestRoll() public view returns (uint256 d20result) {
        return d20Results[d20Results.length - 1];
    }
}

```

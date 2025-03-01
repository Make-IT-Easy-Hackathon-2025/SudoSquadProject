import React, { useState, useEffect, useRef } from 'react';
import { View, Text, TouchableOpacity, StyleSheet, Dimensions } from 'react-native';

export const ReactionGameWidget = () => {
  // Game states
  const [gameActive, setGameActive] = useState(false);
  const [circleVisible, setCircleVisible] = useState(false);
  const [position, setPosition] = useState({ x: 0, y: 0 });
  const [reactionTime, setReactionTime] = useState<number | null>(null);
  const [bestTime, setBestTime] = useState<number | null>(null);
  const [countdown, setCountdown] = useState(3);
  
  // References with proper TypeScript types
  const appearTimeRef = useRef<number | null>(null);
  const countdownTimerRef = useRef<NodeJS.Timeout | null>(null);
  
  // Force a default width if the measurement fails
  const [gameAreaSize, setGameAreaSize] = useState({
    width: Dimensions.get('window').width - 32, // Subtract padding
    height: 300,
    // width: 400,
    // height: 400
  });
  
  // Circle properties
  const CIRCLE_SIZE = 50;
  
  // Start the game
  const startGame = () => {
    setGameActive(true);
    setReactionTime(null);
    scheduleNextCircle();
  };
  
  // Stop the game
  const stopGame = () => {
    setGameActive(false);
    setCircleVisible(false);
    if (countdownTimerRef.current !== null) {
      clearInterval(countdownTimerRef.current);
      countdownTimerRef.current = null;
    }
  };
  
  // Generate random position within the game area
  const generateRandomPosition = () => {
    // Ensure we have a reasonable width to work with
    const areaWidth = gameAreaSize.width > 0 ? gameAreaSize.width : Dimensions.get('window').width - 32;
    const areaHeight = gameAreaSize.height;
    
    // Calculate maximum positions
    const maxX = Math.max(0, areaWidth - CIRCLE_SIZE);
    const maxY = Math.max(0, areaHeight - CIRCLE_SIZE);
    
    // console.log('Game area size:', {width: areaWidth, height: areaHeight});
    // console.log('Max position values:', { maxX, maxY });
    
    // Ensure we get positive values
    const x = Math.min(maxX, Math.max(0, Math.floor(Math.random() * maxX)));
    const y = Math.min(maxY, Math.max(0, Math.floor(Math.random() * maxY)));
    
    // console.log('Generated position:', { x, y });
    return { x, y };
  };
  
  // Schedule the next circle to appear
  const scheduleNextCircle = () => {
    setCircleVisible(false);
    setCountdown(3);
    
    // Clear any existing timer first
    if (countdownTimerRef.current !== null) {
      clearInterval(countdownTimerRef.current);
    }
    
    // Start countdown
    countdownTimerRef.current = setInterval(() => {
      setCountdown((prev) => {
        if (prev <= 1) {
          if (countdownTimerRef.current !== null) {
            clearInterval(countdownTimerRef.current);
            countdownTimerRef.current = null;
          }
          // Add a small delay before showing the circle
          setTimeout(showCircle, 100);
          return 0;
        }
        return prev - 1;
      });
    }, 1000);
  };
  
  // Show the circle at a random position
  const showCircle = () => {
    // console.log('Showing circle');
    const newPosition = generateRandomPosition();
    
    // Set both the position and visibility in a more reliable sequence
    setPosition(newPosition);
    
    // Set appear time before setting visibility
    appearTimeRef.current = Date.now();
    
    // Set circle to visible
    setCircleVisible(true);
    
    // Double check after a small delay that the circle is actually visible
    setTimeout(() => {
      // console.log('After delay, circle visible:', circleVisible);
      // If not visible for some reason, force it visible
      if (!circleVisible) {
        // console.log('Forcing circle to be visible');
        setCircleVisible(true);
      }
    }, 200);
  };
  
  // Handle circle tap
  const handleTap = () => {
    // console.log('Circle tapped!');
    
    if (!circleVisible) {
      // console.log('Circle not visible, ignoring tap');
      return;
    }
    
    const tapTime = Date.now();
    if (appearTimeRef.current !== null) {
      const currentReactionTime = tapTime - appearTimeRef.current;
      
      setReactionTime(currentReactionTime);
      setCircleVisible(false);
      
      // Update best time if needed
      if (bestTime === null || currentReactionTime < bestTime) {
        setBestTime(currentReactionTime);
      }
      
      // Schedule next circle using setTimeout
      setTimeout(scheduleNextCircle, 1000);
    }
  };
  
  // Measure the game area on layout
  const onGameAreaLayout = (event: any) => {
    const { width, height } = event.nativeEvent.layout;
    // const width = 400;
    // const height = 400;
    // console.log('Game area measured:', { width, height });
    
    // Only update if we got a valid width
    if (width > 0) {
      setGameAreaSize({ width, height });
    } else {
      console.warn('Invalid game area width measured. Using fallback.');
    }
  };
  
  // Effect to update state and check rendering
  useEffect(() => {
    if (circleVisible) {
      // console.log('Circle is now visible in state');
    }
  }, [circleVisible]);
  
  // Clean up timers when component unmounts
  useEffect(() => {
    return () => {
      if (countdownTimerRef.current !== null) {
        clearInterval(countdownTimerRef.current);
        countdownTimerRef.current = null;
      }
    };
  }, []);
  
  return (
    <View style={styles.container}>
      <Text style={styles.title}>Reaction Time Game</Text>
      
      {gameActive ? (
        <View style={{flex: 1, justifyContent: 'center', alignItems: 'center'}}>
          <View 
            style={styles.gameArea}
            onLayout={onGameAreaLayout}
          >
            {/* Force the circle to render when it should be visible */}
            {circleVisible ? (
              <TouchableOpacity
                style={[
                  styles.circle,
                  { 
                    left: position.x, 
                    top: position.y,
                    backgroundColor: '#3498db', // Ensure color is visible
                  }
                ]}
                onPress={handleTap}
                activeOpacity={0.9}
              />
            ) : null}
            
            {!circleVisible && countdown > 0 && (
              <View style={styles.countdownContainer}>
                <Text style={styles.countdown}>{countdown}</Text>
              </View>
            )}
          </View>
          
          <View style={styles.stats}>
            <Text style={styles.statText}>
              {reactionTime !== null 
                ? `Last: ${reactionTime} ms` 
                : 'Tap the circle when it appears!'}
            </Text>
            
            {bestTime !== null && (
              <Text style={styles.statText}>Best: {bestTime} ms</Text>
            )}
          </View>
          
          <TouchableOpacity
            style={[styles.button, styles.stopButton]}
            onPress={stopGame}
          >
            <Text style={styles.buttonText}>Stop Game</Text>
          </TouchableOpacity>
        </View>
      ) : (
        <View style={styles.startContainer}>
          <Text style={styles.instructions}>
            Tap the circles as quickly as you can when they appear.
            The game will measure your reaction time in milliseconds.
          </Text>
          
          <TouchableOpacity
            style={[styles.button, styles.startButton]}
            onPress={startGame}
          >
            <Text style={styles.buttonText}>Start Game</Text>
          </TouchableOpacity>
        </View>
      )}
      
      {/* Debug info */}
      {/* <View style={styles.debugInfo}>
        <Text>Game Active: {gameActive ? 'Yes' : 'No'}</Text>
        <Text>Circle Visible: {circleVisible ? 'Yes' : 'No'}</Text>
        <Text>Countdown: {countdown}</Text>
        <Text>Position: {JSON.stringify(position)}</Text>
        <Text>Game Area: {JSON.stringify(gameAreaSize)}</Text>
      </View> */}
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    // flex: 1,
    padding: 16,
    // alignItems: 'center',
    // border segít a határok látványosabbá tételében
    borderColor: 'red',
    borderWidth: 1,
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 16,
  },
  gameArea: {
    width: '100%',
    // Dinamikus magasság, hogy kitöltse a rendelkezésre álló helyet
    flex: 1,
    backgroundColor: '#f0f0f0',
    borderRadius: 8,
    position: 'relative', // Fontos, hogy a gyermekek ezen belül legyenek abszolút pozicionálva
    overflow: 'hidden',  // Biztosítja, hogy semmi ne lógjon ki
    marginBottom: 16,
    borderWidth: 1,
    borderColor: '#ddd',
    // borderColor: '#aaa'
  },
  circle: {
    width: 50,
    height: 50,
    borderRadius: 25,
    backgroundColor: '#3498db',
    position: 'absolute',
    borderWidth: 2,
    borderColor: 'black',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.5,
    shadowRadius: 3.84,
    elevation: 5,
  },
  stats: {
    marginBottom: 16,
    alignItems: 'center',
  },
  statText: {
    fontSize: 18,
    marginVertical: 4,
  },
  button: {
    paddingVertical: 12,
    paddingHorizontal: 32,
    borderRadius: 24,
    alignItems: 'center',
    justifyContent: 'center',
  },
  startButton: {
    backgroundColor: '#2ecc71',
  },
  stopButton: {
    backgroundColor: '#e74c3c',
  },
  buttonText: {
    color: 'white',
    fontSize: 16,
    fontWeight: 'bold',
  },
  startContainer: {
    alignItems: 'center',
  },
  instructions: {
    textAlign: 'center',
    marginBottom: 24,
    fontSize: 16,
    lineHeight: 22,
  },
  countdownContainer: {
    position: 'absolute',
    top: 0,
    left: 0,
    right: 0,
    bottom: 0,
    justifyContent: 'center',
    alignItems: 'center',
  },
  countdown: {
    fontSize: 48,
    fontWeight: 'bold',
    color: '#7f8c8d',
  },
  debugInfo: {
    marginTop: 20,
    padding: 10,
    backgroundColor: '#f8f9fa',
    borderRadius: 5,
    width: '100%',
  }
});

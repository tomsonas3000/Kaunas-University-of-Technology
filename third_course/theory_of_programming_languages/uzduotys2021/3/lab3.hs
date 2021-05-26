import Control.Monad
import System.Exit (exitSuccess)

main = do
    lengthStr <- getLine
    when (lengthStr == "0") $ do
        exitSuccess
    let lengthInt = read lengthStr :: Int
    positions <- getLine
    let minDistance = getShortestDistance positions lengthInt (-lengthInt) (-lengthInt) 0
    putStrLn ("Minimum distance between a restaurant and a drugstore: " ++ (show minDistance)) 
    putStrLn "------------------"
    main

getShortestDistance :: [Char] -> Int -> Int -> Int -> Int -> Int
getShortestDistance positions minDistance lastRestaurant lastDrugstore index
    | ((length positions) == (index )) = minDistance

    | ((positions !! index) == 'Z') = 0
    
    | ((positions !! index) == 'R') = do
        let updatedMin = min minDistance (index - lastDrugstore)
        let updatedLastRestaurant = index
        getShortestDistance positions updatedMin updatedLastRestaurant lastDrugstore (index + 1)

    | ((positions !! index) == 'D') = do
        let updatedMin = min minDistance (index - lastRestaurant)
        let updatedLastDrugstore = index
        getShortestDistance positions updatedMin lastRestaurant updatedLastDrugstore (index + 1)

    | otherwise = getShortestDistance positions minDistance lastRestaurant lastDrugstore (index + 1)
# GameOfLife
Conway's Game of Life implemented using C#

Algorithm: 
The first idea that comes to mind is to use For loops and iterate over every single cell in an effort to determine the game universe's next generation state. However, I thought it would be more efficient to devise an algorithm that determines candidate cells that could potentially change in the next generation, and then to determine the next states of only these cells. Such candidate cells would then be any currently alive cells and their neighbours. I also realized that it is not necessary to store the state of cells (Alive/Dead) as these can be calculated for the next generation using the current numbers of their alive neighbours and the Game of Life's rules. I then used two Dictionary  to represent this data, one for alive cells and one for dead cells of the current generation.

Patterns: I've made use of the Decorator (although not in its strict form as you know using inheritance is forbidden on Structs) and Strategy patterns to add some level of separation of concerns and protected variations, although the current requirements don't strictly require these (in an agile sense). I also hoped to use an Adapter to convert the model representation of the GameUniverse (which holds only Alive Cells and the game board's dimensions into a View of a different interface that can be consumed by the Simulation project, however, I've just used a rudimentary Console application without an adapter due to time constraints.

Aspects: I haven't written cross-cutting concerns into the codebase like checks for defensive programming, Logging, Exception handling etc. as I try to implement them using AOP and a tool like PostSharp. I do edge case checks on the input parameters to the GameUniverse and on the algorithm itself.

Execution: The Simulation app hosts the Console application where you can see the results of the code. If you restore the packages and hit Run, you should be able to see an oscillatory pattern on the Game of Life screen.

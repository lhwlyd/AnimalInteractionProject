Hi, this project is under development by Carlos Chulo @ cchulo@ucsd.edu and 
Roy Luo @ yil623@ucsd.edu under the supervision of Dr. Jürgen P. Schulze 
jschulze@ucsd.edu

Feel free to contact us if you'd like to contribute towards this project, have
ideas on what should get further implemented, or want to learn more about 
some implementation details!

Here're some tips (we'll move these to somewhere else when they get long!)

How to create a fully funcitonal new State:

1. Make a new state script that implements the IState interface, 
	implement all the methods (Enter(), Execute(), and Exit() )

2. The way the statemachine works is it runs the new State's Enter() 
	when the new state is introduced, run the Execute() of this state inside 
	every Update(), and run the Exit() when some other state is introduced.

For example, the WanderAround State does this:
	Enter(): Set a new location that the animal should move to initially.
	Execute(): Keep moving to the current destination, or introduce a
			new destination if the last one is finished.
	Exit(): Set the NavMeshAgent's destination to the current loc so it
		doesn't try to go to the old destination.

3. Usually we'll need access to some variables, make sure to set such vars
	as private, and use a Constructor to pass them in.

4. Implement the statmachine's logic in the actual animal class(Turtle), as
	it depends on the animal how the would react to different events.

5. Make use of SetBusy() in the baseAnimal class. The principle is to make all
	activities related to basic needs as "Busy States", by setting the
	animal to be a proper busy state using SetBusy(). E.g. SearchingForFood,
	Resting, Grabbed...

	Other states like WanderAround, PlayWithLaserLight, etc shouldn't be
	set as a busy state. They should call ExitBusy() in Enter() to make sure
	animals can turn to satisfy their basic needs when they are doing such
	activities.

6. Sometimes it will be a better idea to compose a mini-statemachine inside a 
	state, becuase it offers more flexibilities and won't affect the main
	stateMachine's memory of the last state, or simply because some behaviours
	compose other minor behaviours(e.g. SearchForFood composes Resting and 
	WanderAround).

7. To create detailed states that only suits a certain type of animal(e.g. 
	RabitWanderAround, which should be implemented as hopping instead
	of running), extends the general states and override/modify.


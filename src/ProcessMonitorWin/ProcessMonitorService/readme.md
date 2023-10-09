We want some kind of loop that keeps track of running services.

In a perfect world we would tick each time something interesting happens.

But let's start easy with a timer based loop.



-- 

we are interested in two things

- what is currently running
- what is the information for a binary (regardless of how many copies are running in memory)
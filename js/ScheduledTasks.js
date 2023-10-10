/** Stores the list of currently scheduled tasks in memory */
var ScheduledTasks = [];

/**
 * Should be self-explanatory. :)
 * @remarks Okay, I won't be like that (lol).  This should be the first thing to
 * run, before any of the other task-scheduling functions (below) are called.  I
 * recommend doing it in window.onload, or some other initial setup script you
 * may have.
 */
function StartScheduledTaskLoop() {
	setInterval(function() {
		var i = 0, j, r, now = new Date();
		now.setMilliseconds(0);
		now.setSeconds(0);
		now = now.getTime();
		for (; i<ScheduledTasks.length; i++) {
			if (ScheduledTasks[i].when == now) {
				ScheduledTasks[i].onstart(ScheduledTasks[i].what);
				ScheduledTasks.splice(i, 1);
				continue;
			}
			for (j=0; j<ScheduledTasks[i].reminders.length; j++) {
				r = ScheduledTasks[i].reminders[j];
				if (r.done) continue;
				if (ScheduledTasks[i].when - r.when == now) {
					r.onstart(ScheduledTasks[i].what);
					r.done = 1;
				}
			}
		}
	}, 1000);
}

/**
 * Creates a new scheduled task and adds it ot the list
 * @param {string|Date} when The date/time the task should happen
 * @param {string} what A brief summary of the task
 * @param {function} onstart The code to run at the scheduled time.
 * Your summary (the "what" parameter) is passed to his function.
 * @returns {ScheduledTask} I'm not wasting bytes on defining a class,
 * but this object has the following: when (number), what (string),
 * onstart (function), and reminders (Array<Reminder>); see the reminder
 * functions for that
 * @throws If you try to schedule a task that is in the past, it lets you
 * know I have not yet cracked how to travel back in time. :D
 */
function AddScheduledTask(when, what, onstart) {
	if (typeof(when) === 'string') when = new Date(when);
	when.setMilliseconds(0);
	when.setSeconds(0);
	if (when < new Date()) throw "Can't schedule tasks in the past.";
	when = when.getTime();
	var j = { when: when, what: what, onstart: onstart, reminders:[] };
	ScheduledTasks.push(j);
	return j;
}

/**
 * Creates a new scheduled task and adds it ot the list
 * @param {ScheduledTask} which The task to be edited
 * @param {string|Date} [when] The date/time the task should happen
 * @param {string} [what] A brief summary of the task
 * @param {function} [onstart] The code to run at the scheduled time.
 * @param {truthy} [redo] If set to 1, mark all the task's reminders as "not
 * done"; this is useful if, for example, you reschedule a task for later but
 * the reminder function already ran.  Default is zero (false).
 * @throws If you try to schedule a task that is in the past, it lets you
 * know I have not yet cracked how to travel back in time. :D
 * @remarks
 * 1. If you set any of these to an empty string, it will keep the old
 * value.
 * 2. Note also that there is no DeleteScheduledTask function; this is because
 * you can just do ScheduledTasks.splice(index, 1) or anything else; it's just a
 * basic JS array.  Add and edit are important to make sure the date and onstart
 * callback stuff is right, but deleting is an easy one-liner.
 */
function EditScheduledTask(which, when = "", what = "", onstart = "", undo = 0) {
	// Edit "when" if the user wants
	if (when !== "") {
		if (typeof(when) === 'string') when = new Date(when);
		when.setMilliseconds(0);
		when.setSeconds(0);
		if (when < new Date()) throw "Can't schedule tasks in the past.";
		which.when = when.getTime();
	}
	
	// Edit "what" and "onstart" if the user wants
	if (what !== "") which.what = what;
	if (onstart !== "") which.onstart = onstart;
	
	// And if the user wants, mark all the reminders as not-done
	if (undo) for (var i=0; i<which.reminders.length; i++) which.reminders[i].done = 0;
}

/**
 * Adds a reminder to a scheduled task
 * @param {ScheduledTsk} An object returned by AddScheduledTask
 * @param {number}  minutes The number of minutes before the task - i.e. put 15
 * for 15 minutes, 60 for an hour, etc.
 * @param {function} onstart The code to run when the reminder should be sent;
 * this does the actual "reminding" (with whatever UI stuff you created).
 * @returns {Reminder} An object with: when (number); minutes converted to
 * milliseconds, done (number), 0 for false, and onstart (function); the onstart
 * function you passed to this
 * @throws if the reminder time is in the past
 */
function AddScheduledTaskReminder(task, minutes, onstart) {
	var r = { onstart: onstart, when: minutes * 60000, done: 0 };
	if (task.when - r.when < new Date()) throw "Can't schedule tasks in the past.";
	task.reminders.push(r);
	return r;
}

/**
 * Edits a reminder
 * @param {ScheduledTask} task An object returned by AddScheduledTask
 * @param {Reminder} r An object returned by AddScheduledTaskReminder
 * @param {number} [minutes] The number of minutes before the task
 * @param {function} onstart The code to run when the reminder should be sent;
 * this does the actual "reminding" (with whatever UI stuff you created).
 * @throws if the reminder time is in the past
 */
function EditScheduledTaskReminder(task, r, minutes = 0, onstart = "") {
	if (minutes) r.when = minutes * 60000;
	if (task.when - r.when < new Date()) throw "Can't schedule tasks in the past.";
	if (onstart !== "") r.onstart = onstart;
}

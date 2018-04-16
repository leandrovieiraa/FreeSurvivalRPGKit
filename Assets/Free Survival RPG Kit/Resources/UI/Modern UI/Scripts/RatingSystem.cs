using UnityEngine;
using System.Collections;

public class RatingSystem : MonoBehaviour {

	[Header("ANIMATORS")]
	public Animator ratingAnimator;

	[Header("VARIABLES")]
	[Range(1, 5)] public int startupRating = 1;

	void Start ()
	{
		if(startupRating == 1)
		{
			ratingAnimator.Play("Rating1_f5");
			startupRating = 1;
		}

		else if(startupRating == 2)
		{
			ratingAnimator.Play("Rating2_f1");
			startupRating = 2;
		}

		else if(startupRating == 3)
		{
			ratingAnimator.Play("Rating3_f1");
			startupRating = 3;
		}

		else if(startupRating == 4)
		{
			ratingAnimator.Play("Rating4_f1");
			startupRating = 4;
		}

		else if(startupRating == 5)
		{
			ratingAnimator.Play("Rating5_f1");
			startupRating = 5;
		}

	}

	public void setRating (float btnRating)
	{
		// START OF RATING 1 //
		if(btnRating == 1 && startupRating == 2)
		{
			ratingAnimator.Play("Rating1_f2");
			startupRating = 1;
		}

		else if(btnRating == 1 && startupRating == 3)
		{
			ratingAnimator.Play("Rating1_f3");
			startupRating = 1;
		}

		else if(btnRating == 1 && startupRating == 4)
		{
			ratingAnimator.Play("Rating1_f4");
			startupRating = 1;
		}

		else if(btnRating == 1 && startupRating == 5)
		{
			ratingAnimator.Play("Rating1_f5");
			startupRating = 1;
		}
		// END OF RATING 1 //

		// START OF RATING 2 //
		else if(btnRating == 2 && startupRating == 1)
		{
			ratingAnimator.Play("Rating2_f1");
			startupRating = 2;
		}

		else if(btnRating == 2 && startupRating == 3)
		{
			ratingAnimator.Play("Rating2_f3");
			startupRating = 2;
		}

		else if(btnRating == 2 && startupRating == 4)
		{
			ratingAnimator.Play("Rating2_f4");
			startupRating = 2;
		}

		else if(btnRating == 2 && startupRating == 5)
		{
			ratingAnimator.Play("Rating2_f5");
			startupRating = 2;
		}
		// END OF RATING 2 //

		// START OF RATING 3 //
		else if(btnRating == 3 && startupRating == 1)
		{
			ratingAnimator.Play("Rating3_f1");
			startupRating = 3;
		}

		else if(btnRating == 3 && startupRating == 2)
		{
			ratingAnimator.Play("Rating3_f2");
			startupRating = 3;
		}

		else if(btnRating == 3 && startupRating == 4)
		{
			ratingAnimator.Play("Rating3_f4");
			startupRating = 3;
		}

		else if(btnRating == 3 && startupRating == 5)
		{
			ratingAnimator.Play("Rating3_f5");
			startupRating = 3;
		}
		// END OF RATING 3 //

		// START OF RATING 4 //
		else if(btnRating == 4 && startupRating == 1)
		{
			ratingAnimator.Play("Rating4_f1");
			startupRating = 4;
		}

		else if(btnRating == 4 && startupRating == 2)
		{
			ratingAnimator.Play("Rating4_f2");
			startupRating = 4;
		}

		else if(btnRating == 4 && startupRating == 3)
		{
			ratingAnimator.Play("Rating4_f3");
			startupRating = 4;
		}

		else if(btnRating == 4 && startupRating == 5)
		{
			ratingAnimator.Play("Rating4_f5");
			startupRating = 4;
		}
		// END OF RATING 4 //

		// START OF RATING 5 //
		else if(btnRating == 5 && startupRating == 1)
		{
			ratingAnimator.Play("Rating5_f1");
			startupRating = 5;
		}

		else if(btnRating == 5 && startupRating == 2)
		{
			ratingAnimator.Play("Rating5_f2");
			startupRating = 5;
		}

		else if(btnRating == 5 && startupRating == 3)
		{
			ratingAnimator.Play("Rating5_f3");
			startupRating = 5;
		}

		else if(btnRating == 5 && startupRating == 4)
		{
			ratingAnimator.Play("Rating5_f4");
			startupRating = 5;
		}
		// END OF RATING 5 //
	}
}

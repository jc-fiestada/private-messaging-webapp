import * as animation from "../animation/animation.js";


export function statusCode(statusCode : number, errors? : string[] | null){

    // TODO: MAYBE UPDATE THE STYLE TO HAVE AN ERROR OR SUM LIKE DAT IDK
    // TS IS REPETITIVE, 

    
    if (statusCode === 400 && errors != null){
        errors?.forEach(error => {
            animation.showToast(error);
        });
    }

    if (statusCode === 422 && errors != null){
        errors?.forEach(error => {
            animation.showToast(error);
        });
    }

    if (statusCode === 401 && errors != null){
        errors?.forEach(error => {
            animation.showToast(error);
        });

        console.log("trigger 401 animation");
    }
}
import * as animation from "../animation/animation.js";


export function statusCode(statusCode : number, errors? : string[] | null){
    if (statusCode === 400 && errors != null){
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
export class ApiResponse{
    public errors: string[] | null;
    public data: any | null;
    public message: any | null;


    constructor(errors: string[] | null, data: any | null, message: string | null){
        this.errors = errors;
        this.data = data;
        this.message = message;
    }
}
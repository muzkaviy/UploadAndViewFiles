import { FileToCreate } from './_interfaces/fileToCreate.model';
import { File } from './_interfaces/file.model';
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {
  public isCreate: boolean;
  public file: FileToCreate;
  public files: File[] = [];
  public fileTypes: string[] = [];
  public errorMessage: '';
  public response: { 
    dbPath: '', 
    fileLength: 0,
    fileType: '',
    fileExtension: ''
  }

  constructor(private http: HttpClient){}

  ngOnInit(){
    this.errorMessage = '';
    this.getFiles();
    this.getCurrentFileTypes();
  }

  public onCreate = () => {
    this.errorMessage = '';

    this.file = {
      filePath: this.response.dbPath,
      fileSize: this.response.fileLength,
      fileType: this.response.fileType,
      fileExtension: this.response.fileExtension
    }

    this.http.post('https://localhost:5001/api/files/create', this.file)
    .subscribe(
      data => {
        this.getFiles();
      },
      error => {
        this.errorMessage = error.error.detail
      }
    );

    this.response.dbPath = '';
  }

  private getFiles = () => {
    this.http.get('https://localhost:5001/api/files/get_all')
    .subscribe(res => {
      this.files = res as File[];
    });
  }

  private getCurrentFileTypes = () => {
    this.http.get('https://localhost:5001/api/files/get_current_file_types')
    .subscribe(res => {
      this.fileTypes = res as string[];
    });
  }

  public uploadFinished = (event) => {
    this.errorMessage = '';
    this.response = event;
  }

  public createFilePath = (serverPath: string) => {
    return `https://localhost:5001/${serverPath}`;
  }
}

import fs from 'fs'


export const getStudents = () => {
  const studentsJSON = fs.readFileSync('students/students.json')
  return JSON.parse(studentsJSON)
}



export const writeStudents = students => {
  fs.writeFileSync('students/students.json', JSON.stringify(students))
}

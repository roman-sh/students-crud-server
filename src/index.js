import express, { json } from 'express'
import morgan from 'morgan'
import cors from 'cors'
import { getStudents, writeStudents } from './students.service.js'


const PORT = process.env.PORT || 3500

const app = express()
app.use(cors())
app.use(json())
app.use(morgan('dev'))


app.get('/students', async (req, res) => {
  const students = await getStudents()
  res.status(200).send(students)
})


// add or update
app.post('/student', async (req, res) => {
  const students = await getStudents()
  const _id = req.body._id
  if (!_id) {  // new student
    students.push({ ...req.body, _id: getRandomId() })
  }
  else {
    const index = students.findIndex(student => _id === student._id)
    students[index] = req.body
  }
  await writeStudents(students)
  res.status(200).send(students)
})


app.delete('/student/:_id', async (req, res) => {
  const _id = req.params._id
  const studentsBefore = await getStudents()
  const studentsAfter = studentsBefore.filter(student => _id !== student._id)
  await writeStudents(studentsAfter)
  res.status(200).send(studentsAfter)
})


app.listen(PORT, () => console.log(`App is running at http://localhost:${PORT}`))


function getRandomId() {
  return String(Math.floor(Math.random() * 999 + 1))
} 

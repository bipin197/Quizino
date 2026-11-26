# Variables
$AWS_ACCOUNT_ID = "<aws_account_id>"
$AWS_REGION = "<region>"
$QUIZAPI_REPO = "quizapi"
$QUESTIONAPI_REPO = "questionapi"

# Authenticate Docker to ECR
aws ecr get-login-password --region $AWS_REGION | docker login --username AWS --password-stdin "$AWS_ACCOUNT_ID.dkr.ecr.$AWS_REGION.amazonaws.com"

# Build Docker images
docker build -t $QUIZAPI_REPO -f .\QuizApi\Dockerfile .
docker build -t $QUESTIONAPI_REPO -f .\QuestionApi\Dockerfile .

# Tag images for ECR
docker tag $QUIZAPI_REPO:latest "$AWS_ACCOUNT_ID.dkr.ecr.$AWS_REGION.amazonaws.com/$QUIZAPI_REPO:latest"
docker tag $QUESTIONAPI_REPO:latest "$AWS_ACCOUNT_ID.dkr.ecr.$AWS_REGION.amazonaws.com/$QUESTIONAPI_REPO:latest"

# Push images to ECR
docker push "$AWS_ACCOUNT_ID.dkr.ecr.$AWS_REGION.amazonaws.com/$QUIZAPI_REPO:latest"
docker push "$AWS_ACCOUNT_ID.dkr.ecr.$AWS_REGION.amazonaws.com/$QUESTIONAPI_REPO:latest"